using Microsoft.AspNetCore.Mvc;
using StockManager.API.MicroServices.AnalysisService;
using StockManager.API.ServiceErrors;
using StockManager.Contracts.Analysis;
using StockManager.Contracts.Error;
using StockManager.API.MicroServices.AnalysisService.Result;
using StockManager.API.Mapper;
//using StockManager.TwelveDataDotNet.Library.ResponseModels;

namespace StockManager.API.Controllers
{
    [ApiController]
    [Route("analysis")]
    public class AnalysisController : ControllerBase
    {

        private readonly IAnalysisService _analysisService = new AnalysisService();

        [HttpGet("get-stock-analysis")]
        public async Task<IActionResult> GetStockAnalysis(GetStockAnalysisRequest req) {
            var result = await _analysisService.GetStockAnalysisDataAsync(req);
            if (result.ResponseStatus == ResponseStatus.Ok) {
                GetStockAnalysisResponse response = Mapper.AnalysisMapper.StockToResponse(result);
                return Ok(response);
            }
            else
            {
                if (result.ResponseStatus == ResponseStatus.TwelveDataAPIError) {
                    Error error = AnalysisError.NotFound(result.ResponseMessage);
                    ErrorResponse errorResponse = ErrorMapper.ToResponse(error);
                    return NotFound(errorResponse);
                } else {
                    Error error = AnalysisError.APIError(result.ResponseMessage);
                    ErrorResponse errorResponse = ErrorMapper.ToResponse(error);
                    return BadRequest(errorResponse);
                }
            }
        }

        [HttpGet("get-portfolio-analysis")]
        public async Task<IActionResult> GetPortfolioAnalysis(GetPortfolioAnalysisRequest req) {
            var result = await _analysisService.GetPortfolioAnalysisDataAsync(req);
            if (result.ResponseStatus == ResponseStatus.Ok) {
                GetPortfolioAnalysisResponse response = Mapper.AnalysisMapper.PortfolioToResponse(result);
                return Ok(response);
            } else {
                if (result.ResponseStatus == ResponseStatus.TwelveDataAPIError) {
                    Error error = AnalysisError.NotFound(result.ResponseMessage);
                    ErrorResponse errorResponse = ErrorMapper.ToResponse(error);
                    return NotFound(errorResponse);
                } else {
                    Error error = AnalysisError.NotFound(result.ResponseMessage);
                    ErrorResponse errorResponse = ErrorMapper.ToResponse(error);
                    return BadRequest(errorResponse);
                    

                }

            }

        }
    }
}