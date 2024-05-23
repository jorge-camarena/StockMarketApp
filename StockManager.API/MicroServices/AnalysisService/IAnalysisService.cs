using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StockManager.API.MicroServices.AnalysisService.Result;
using StockManager.Contracts.Analysis;

namespace StockManager.API.MicroServices.AnalysisService
{
    public interface IAnalysisService
    {
        Task<StockAnalysisResult> GetStockAnalysisDataAsync(GetStockAnalysisRequest req);

        Task<PortfolioAnalysisResult> GetPortfolioAnalysisDataAsync(GetPortfolioAnalysisRequest req);

        Task<StockAnalysisResult> GetSingleStockAnalysisAsync(string symbol, string interval, string startDate, string endDate);
    }
}