using Microsoft.AspNetCore.Mvc;
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

        private readonly IStockService _stockService = new StockService(new());


        [HttpPost("add-stock")]
        public IActionResult AddStock(AddStockRequest req) {
            var result = _stockService.AddStock(req);
            if (result.Error is not null) {
                Error error = result.Error;
                ErrorResponse errorResponse = new ErrorResponse(
                    ErrorCode: error.Code,
                    Description: error.Description
                );
                if (error.Code == "Stock.NoAssociatedPortfolioFound") {
                    return NotFound(errorResponse);
                } else {
                    return BadRequest(errorResponse);
                }

            }
            Stock? stock = result.Value;
            StockResponse response = new StockResponse(
                Id: stock.Id,
                StockSymbol: stock.Symbol,
                AmountInvested: stock.AmountInvested
            );
            return Ok(response);
        }
        [HttpGet("get-stock")]
        public IActionResult GetStock(Guid id) {
            var result = _stockService.GetStock(id);
            if (result.Error is not null) {
                Error error = result.Error;
                ErrorResponse errorResponse = new ErrorResponse(
                    ErrorCode: error.Code,
                    Description: error.Description
                );
                if (error.Code == "Stock.NotFound") {
                    return NotFound(errorResponse);
                } else {
                    return BadRequest(errorResponse);
                }

            }
            Stock? stock = result.Value;
            StockResponse response = new StockResponse(
                Id: stock.Id,
                StockSymbol: stock.Symbol,
                AmountInvested: stock.AmountInvested
            );
            return Ok(response);
        }
        [HttpDelete("delete-stock")]
        public IActionResult DeleteStock(Guid id) {
            var result = _stockService.DeleteStock(id);
            if (result.Error is not null) {
                Error error = result.Error;
                ErrorResponse errorResponse = new ErrorResponse(
                    ErrorCode: error.Code,
                    Description: error.Description
                );
                if (error.Code == "Stock.NotFound") {
                    return NotFound(errorResponse);
                } else {
                    return BadRequest(errorResponse);
                }

            }
            Stock? stock = result.Value;
            StockResponse response = new StockResponse(
                Id: stock.Id,
                StockSymbol: stock.Symbol,
                AmountInvested: stock.AmountInvested
            );
            return Ok(response);
        } 
    }
}