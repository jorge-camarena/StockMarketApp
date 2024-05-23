using Microsoft.AspNetCore.Mvc;
using StockManager.API.Mapper;
using StockManager.API.MicroServices.StockService;
using StockManager.API.Models;
using StockManager.API.ServiceErrors;
using StockManager.Contracts.Error;
using StockManager.Contracts.Stock;

namespace StockManager.API.Controllers
{
    [ApiController]
    [Route("Stock")]
    public class StockController : ControllerBase
    {

        private readonly IStockService _stockService;

        public StockController(IStockService stockService) {
            _stockService = stockService;
        }

        [HttpPost("add-stock")]
        public async Task<IActionResult> AddStock(AddStockRequest req) {
            var result = await _stockService.AddStock(req);
            if (result._success) {
                Stock? stock = result.Value;
                StockResponse response = StockMapper.ToResponse(stock);
                return Ok(response);
            } else {
                Error error = result.Error;
                ErrorResponse errorResponse = ErrorMapper.ToResponse(error);
                if (error.Code == "Stock.NoAssociatedPortfolioFound" || error.Code == "Stock.InvalidSymbol") {
                    return NotFound(errorResponse);
                } else {
                    return BadRequest(errorResponse);
                }
            }
        }

        [HttpGet("get-stock/{id:Guid}")]
        public IActionResult GetStock(Guid id) {
            var result = _stockService.GetStock(id);
            if (result._success) {
                Stock? stock = result.Value;
                StockResponse response = StockMapper.ToResponse(stock);
                return Ok(response);
            } else {
                Error error = result.Error;
                ErrorResponse errorResponse = ErrorMapper.ToResponse(error);
                if (error.Code == "Stock.NotFound") {
                    return NotFound(errorResponse);
                } else {
                    return BadRequest(errorResponse);
                }
            }
        }

        [HttpDelete("delete-stock/{id:Guid}")]
        public IActionResult DeleteStock(Guid id) {
            var result = _stockService.DeleteStock(id);
            if (result._success) {
                Stock? stock = result.Value;
                StockResponse response = StockMapper.ToResponse(stock);
                return Ok(response);
            } else {
                Error error = result.Error;
                ErrorResponse errorResponse = ErrorMapper.ToResponse(error);
                if (error.Code == "Stock.NotFound") {
                    return NotFound(errorResponse);
                } else {
                    return BadRequest(errorResponse);
                }
            }
        } 
    }
}