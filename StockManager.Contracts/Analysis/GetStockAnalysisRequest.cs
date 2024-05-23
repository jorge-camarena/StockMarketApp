using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockManager.Contracts.Analysis
{
    public record GetStockAnalysisRequest
    (
        string Symbol,
        string Interval,
        string StartDate = "",
        string EndDate = ""
    );
}