using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockManager.Contracts.Analysis
{
    public record GetPortfolioAnalysisResponse
    (
        Guid PortfolioId,
        string PortfolioName,
        string PortfolioType,
        List<GetStockAnalysisResponse> Stocks
    );
}