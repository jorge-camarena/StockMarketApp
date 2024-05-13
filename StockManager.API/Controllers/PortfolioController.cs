using System;
using Microsoft.AspNetCore.Mvc;
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
            if (result.Error is not null) {
                Error error = result.Error;
                ErrorResponse errorResponse = new ErrorResponse(
                    ErrorCode: error.Code,
                    Description: error.Description
                );
                if (error.Code == "Portfolio.NoAssociatedAccountFound") {
                    return NotFound(errorResponse);
                } else {
                    return BadRequest(errorResponse);
                }
            }
            Portfolio? portfolio = result.Value;
            PortfolioResponse response = new PortfolioResponse(
                Id: portfolio.Id,
                PortfolioName: portfolio.PortfolioName,
                PortfolioType: portfolio.PortfolioType
            );
            return Ok (response);
        }

        [HttpGet("get-portfolio")]
        public IActionResult GetPortfolio(Guid id) {
            var result = _portfolioService.GetPortfolio(id);
            if (result.Error is not null) {
                Error error = result.Error;
                ErrorResponse errorResponse = new ErrorResponse(
                    ErrorCode: error.Code,
                    Description: error.Description
                );
                if (error.Code == "Portfolio.NotFound") {
                    return NotFound(errorResponse);
                } else {
                    return BadRequest(errorResponse);
                }
            }
            Portfolio? portfolio = result.Value;
            PortfolioResponse response = new PortfolioResponse(
                Id: portfolio.Id,
                PortfolioName: portfolio.PortfolioName,
                PortfolioType: portfolio.PortfolioType
            );
            return Ok(response);
        }
        [HttpPut("update-portfolio")]
        public IActionResult UpdatePortfolio(UpdatePortfolioRequest req) {
            var result = _portfolioService.UpdatePortfolio(req);
            if (result.Error is not null) {
                Error error = result.Error;
                ErrorResponse errorResponse = new ErrorResponse(
                    ErrorCode: error.Code,
                    Description: error.Description
                );
                if (error.Code == "Portfolio.NotFound") {
                    return NotFound(errorResponse);
                } else {
                    return BadRequest(errorResponse);
                }
            }
            Portfolio? portfolio = result.Value;
            PortfolioResponse response = new PortfolioResponse(
                Id: portfolio.Id,
                PortfolioName: portfolio.PortfolioName,
                PortfolioType: portfolio.PortfolioType
            );
            return Ok(response);
        }
        [HttpDelete("delete-portfolio")]
        public IActionResult DeletePortfolio(Guid id) {
            var result = _portfolioService.DeletePortfolio(id);
            if (result.Error is not null) {
                Error error = result.Error;
                ErrorResponse errorResponse = new ErrorResponse(
                    ErrorCode: error.Code,
                    Description: error.Description
                );
                if (error.Code == "Portfolio.NotFound") {
                    return NotFound(errorResponse);
                } else {
                    return BadRequest(errorResponse);
                }
            }
            Portfolio? portfolio = result.Value;
            PortfolioResponse response = new PortfolioResponse(
                Id: portfolio.Id,
                PortfolioName: portfolio.PortfolioName,
                PortfolioType: portfolio.PortfolioType
            );
            return Ok(response);
        }
    }
}