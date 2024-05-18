using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockManager.Contracts.SearchSymbolData
{
    public record GetQuoteRequest
    (
        string Symbol,
        string Interval
    );
}