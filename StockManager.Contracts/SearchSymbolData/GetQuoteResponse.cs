using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockManager.Contracts.SearchSymbolData
{
    public record GetQuoteResponse
    (
        string Symbol,
        string Name,
        string Exchange, 
        string Currency, 
        string DateTime, 
        long TimeStamp,
        double Open,
        double High,
        double Low,
        double Close, 
        long Volume, 
        double PreviousClose, 
        double Change,
        double PercentageChange,
        long AverageVolume
    );
}