using Microsoft.AspNetCore.Mvc;
using StockManager.API.MicroServices.SearchSymbolDataService;
using StockManager.Contracts.Error;
using StockManager.Contracts.SearchSymbolData;
using StockManager.TwelveDataDotNet.Library.ResponseModels;

namespace StockManager.API.Controllers
{
    [ApiController]
    [Route("search")]
    public class SearchSymbolController : ControllerBase
    {
        private readonly ISearchSymbolDataService _searchSymbolDataService = new SearchSymbolDataService();

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
                    ErrorResponse errorResponse = new ErrorResponse(
                        ErrorCode: "Symbol.NotFound",
                        Description: result.ResponseMessage
                    );
                    return NotFound(errorResponse);
                } else {
                    ErrorResponse errorResponse = new ErrorResponse(
                    ErrorCode: "Symbol.APIError",
                    Description: result.ResponseMessage
                    );
                    return BadRequest(errorResponse);
                }
            }
        }

        [HttpGet("quote")]
        public async Task<IActionResult> GetSymbolQuote(GetQuoteRequest req) {
            var result = await _searchSymbolDataService.GetSymbolQuoteAsync(req);
            if (result.ResponseStatus == ResponseStatus.Ok) {
                GetQuoteResponse response = new GetQuoteResponse(
                    Symbol: result.Symbol,
                    Name: result.Name,
                    Exchange: result.Exchange,
                    Currency: result.Currency,
                    DateTime: result.DateTime,
                    TimeStamp: result.TimeStamp,
                    Open: result.Open,
                    High: result.High,
                    Low: result.Low,
                    Close: result.Close,
                    Volume: result.Volume,
                    PreviousClose: result.PreviousClose,
                    Change: result.Change,
                    PercentageChange: result.PercentChange,
                    AverageVolume: result.AverageVolume
                );
                return Ok(response);
            } else {
                if (result.ResponseStatus == ResponseStatus.TwelveDataAPIError) {
                    ErrorResponse errorResponse = new ErrorResponse(
                        ErrorCode: "Symbol.NotFound",
                        Description: result.ResponseMessage
                    );
                    return NotFound(errorResponse);
                } else {
                    ErrorResponse errorResponse = new ErrorResponse(
                        ErrorCode: "Symbol.APIError",
                        Description: result.ResponseMessage
                    );
                    return BadRequest(errorResponse);
                }
            }
        }
        [HttpGet("timeseries")]
        public async Task<IActionResult> GetTimeSeries(GetTimeSeriesRequest req) {
            var result = await _searchSymbolDataService.GetTimeSeriesAsync(req);
            if (result.ResponseStatus == ResponseStatus.Ok) {
                List<TimeSeriesValues> responseTSVals = new List<TimeSeriesValues>();
                foreach (var ts_vals in result.Values) {
                    TimeSeriesValues responseObj = new TimeSeriesValues(
                        Datetime: ts_vals.DateTime,
                        Open: ts_vals.Open,
                        High: ts_vals.High,
                        Low: ts_vals.Low,
                        Close: ts_vals.Close,
                        Volume: ts_vals.Volume
                    );
                    responseTSVals.Add(responseObj);
                }
                GetTimeSeriesResponse respose = new GetTimeSeriesResponse(
                    Symbol: result.Symbol,
                    Interval: result.Interval,
                    Currency: result.Currency,
                    Exchange: result.Exchange,
                    Type: result.Type,
                    Values: responseTSVals
                );
                return Ok(respose);
            } else {
                if (result.ResponseStatus == ResponseStatus.TwelveDataAPIError) {
                    ErrorResponse errorResponse = new ErrorResponse(
                        ErrorCode: "Symbol.NotFound",
                        Description: result.ResponseMessage
                    );
                    return NotFound(errorResponse);
                } else {
                    ErrorResponse errorResponse = new ErrorResponse(
                        ErrorCode: "Symbol.NotFound",
                        Description: result.ResponseMessage
                    );
                    return BadRequest(errorResponse);
                }
            }
        }
    }
}