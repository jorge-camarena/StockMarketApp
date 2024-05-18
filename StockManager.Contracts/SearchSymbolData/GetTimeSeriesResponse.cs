using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockManager.Contracts.SearchSymbolData
{
    public record GetTimeSeriesResponse
    (
        string Symbol,
        string Interval,
        string Currency,
        string Exchange,
        string Type,
        List<TimeSeriesValues> Values
    );

    public record TimeSeriesValues 
    (
        string Datetime,
        double Open,
        double High,
        double Low,
        double Close,
        long Volume
    );
}