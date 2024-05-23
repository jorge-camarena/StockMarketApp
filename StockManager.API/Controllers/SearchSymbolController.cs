using Microsoft.AspNetCore.Mvc;
using StockManager.API.Mapper;
using StockManager.API.MicroServices.SearchSymbolDataService;
using StockManager.API.ServiceErrors;
using StockManager.Contracts.Error;
using StockManager.Contracts.SearchSymbolData;
using StockManager.TwelveDataDotNet.Library.ResponseModels;

namespace StockManager.API.Controllers
{
    [ApiController]
    [Route("search")]
    public class SearchSymbolController : ControllerBase
    {
        private readonly ISearchSymbolDataService _searchSymbolDataService;

        public SearchSymbolController(ISearchSymbolDataService searchSymbolDataService) {
            _searchSymbolDataService = searchSymbolDataService;
        }

        [HttpGet("price/{symbol}")]
        public async Task<IActionResult> GetRealTimePrice(string symbol) {
            var result = await _searchSymbolDataService.GetRealTimePriceAsync(symbol);
            if (result.ResponseStatus == ResponseStatus.Ok) {
                GetRealTimePriceResponse response = new GetRealTimePriceResponse(
                Price: result.Price
                );
                return Ok(response);
            } else {
                if (result.ResponseStatus == ResponseStatus.TwelveDataAPIError) {
                    Error error = SearchSymbolError.NotFound(result.ResponseMessage);
                    ErrorResponse errorResponse = ErrorMapper.ToResponse(error);
                    return NotFound(errorResponse);
                } else {
                    Error error = SearchSymbolError.APIError(result.ResponseMessage);
                    ErrorResponse errorResponse = ErrorMapper.ToResponse(error);
                    return BadRequest(errorResponse);
                }
            }
        }

        [HttpGet("quote")]
        public async Task<IActionResult> GetSymbolQuote(GetQuoteRequest req) {
            var result = await _searchSymbolDataService.GetSymbolQuoteAsync(req);
            if (result.ResponseStatus == ResponseStatus.Ok) {
                GetQuoteResponse response = SymbolMapper.QuoteToResponse(result);
                return Ok(response);
            } else {
                if (result.ResponseStatus == ResponseStatus.TwelveDataAPIError) {
                    Error error = SearchSymbolError.NotFound(result.ResponseMessage);
                    ErrorResponse errorResponse = ErrorMapper.ToResponse(error);
                    return NotFound(errorResponse);
                } else {
                    Error error = SearchSymbolError.APIError(result.ResponseMessage);
                    ErrorResponse errorResponse = ErrorMapper.ToResponse(error);
                    return BadRequest(errorResponse);
                }
            }
        }
        
        [HttpGet("timeseries")]
        public async Task<IActionResult> GetTimeSeries(GetTimeSeriesRequest req) {
            var result = await _searchSymbolDataService.GetTimeSeriesAsync(req);
            if (result.ResponseStatus == ResponseStatus.Ok) {
                GetTimeSeriesResponse response = SymbolMapper.TimeSeriesToResponse(result);
                return Ok(response);
            } else {
                if (result.ResponseStatus == ResponseStatus.TwelveDataAPIError) {
                    Error error = SearchSymbolError.NotFound(result.ResponseMessage);
                    ErrorResponse errorResponse = ErrorMapper.ToResponse(error);
                    return NotFound(errorResponse);
                } else {
                    Error error = SearchSymbolError.APIError(result.ResponseMessage);
                    ErrorResponse errorResponse = ErrorMapper.ToResponse(error);
                    return BadRequest(errorResponse);
                }
            }
        }
    }
}