using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StockManager.TwelveDataDotNet.Library.ResponseModels;

namespace StockManager.API.MicroServices.AnalysisService.Result
{
    public class PortfolioAnalysisResult
    {
        public Guid PortfolioId { get; set; }

        public string PortfolioName { get; set; } = string.Empty;

        public string PortfolioType { get; set;} = string.Empty;

        public List<StockAnalysisResult> Stocks { get; set; } = new List<StockAnalysisResult>();

        public ResponseStatus ResponseStatus { get; set; }

        public string ResponseMessage { get; set; } = string.Empty;
    }
}