using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StockManager.Contracts.SearchSymbolData;
using StockManager.TwelveDataDotNet.Library.ResponseModels;

namespace StockManager.API.MicroServices.SearchSymbolDataService
{
    public interface ISearchSymbolDataService
    {
        Task<TwelveDataRealTimePrice> GetRealTimePriceAsync(string symbol);

        Task<TwelveDataQuote> GetSymbolQuoteAsync(GetQuoteRequest req);

        Task<TwelveDataTimeSeries> GetTimeSeriesAsync(GetTimeSeriesRequest req);
    }
}