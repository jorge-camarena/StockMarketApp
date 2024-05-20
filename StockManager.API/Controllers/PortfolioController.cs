using System;
using Microsoft.AspNetCore.Mvc;
using StockManager.API.Mapper;
using StockManager.API.MicroServices.PortfolioService;
using StockManager.API.Models;
using StockManager.API.ServiceErrors;
using StockManager.Contracts.Error;
using StockManager.Contracts.Portfolio;

namespace StockManager.API.Controllers
{
    [ApiController]
    [Route("portfolio")]
    public class PortfolioController : ControllerBase
    {
        private readonly IPortfolioService _portfolioService = new PortfolioService( new());

        [HttpPost("create-portfolio")]
        public IActionResult CreatePortfolio(CreatePortfolioRequest req) {
            var result = _portfolioService.CreatePortfolio(req);
            if (result._success) {
                Portfolio? portfolio = result.Value;
                PortfolioResponse response = PortfolioMapper.ToResponse(portfolio);
                return Ok (response);
            } else {
                Error error = result.Error;
                ErrorResponse errorResponse = ErrorMapper.ToResponse(error);
                if (error.Code == "Portfolio.NoAssociatedAccountFound") {
                    return NotFound(errorResponse);
                } else {
                    return BadRequest(errorResponse);
                }
            }
        }

        [HttpGet("get-portfolio/{id:Guid}")]
        public IActionResult GetPortfolio(Guid id) {
            var result = _portfolioService.GetPortfolio(id);
            if (result._success) {
                Portfolio? portfolio = result.Value;
                PortfolioResponse response = PortfolioMapper.ToResponse(portfolio);
                return Ok(response);
            } else {
                Error error = result.Error;
                ErrorResponse errorResponse = ErrorMapper.ToResponse(error);
                if (error.Code == "Portfolio.NotFound") {
                    return NotFound(errorResponse);
                } else {
                    return BadRequest(errorResponse);
                }
            }
        }

        [HttpPut("update-portfolio")]
        public IActionResult UpdatePortfolio(UpdatePortfolioRequest req) {
            var result = _portfolioService.UpdatePortfolio(req);
            if (result._success) {
                Portfolio? portfolio = result.Value;
                PortfolioResponse response = PortfolioMapper.ToResponse(portfolio);
                return Ok(response);
            } else {
                Error error = result.Error;
                ErrorResponse errorResponse = ErrorMapper.ToResponse(error);
                if (error.Code == "Portfolio.NotFound") {
                    return NotFound(errorResponse);
                } else {
                    return BadRequest(errorResponse);
                }
            }
        }
        
        [HttpDelete("delete-portfolio/{id:Guid}")]
        public IActionResult DeletePortfolio(Guid id) {
            var result = _portfolioService.DeletePortfolio(id);
            if (result._success) {
                Portfolio? portfolio = result.Value;
                PortfolioResponse response = PortfolioMapper.ToResponse(portfolio);
                return Ok(response);
            } else {
                Error error = result.Error;
                ErrorResponse errorResponse = ErrorMapper.ToResponse(error);
                if (error.Code == "Portfolio.NotFound") {
                    return NotFound(errorResponse);
                } else {
                    return BadRequest(errorResponse);
                }
            }
        }
    }
}