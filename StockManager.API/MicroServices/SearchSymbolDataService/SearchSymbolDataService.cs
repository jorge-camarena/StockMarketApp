using StockManager.Contracts.SearchSymbolData;
using StockManager.TwelveDataDotNet.Client;
using StockManager.TwelveDataDotNet.Library.ResponseModels;

namespace StockManager.API.MicroServices.SearchSymbolDataService
{
    public class SearchSymbolDataService : ISearchSymbolDataService
    {
        private readonly  TwelveDataClient _twelveDataClient;

        public SearchSymbolDataService() {
            _twelveDataClient = new TwelveDataClient(new HttpClient(), "f6b555954b7d4b8a80fb2571e8a2f223");
        }

        public async Task<TwelveDataRealTimePrice> GetRealTimePriceAsync(string symbol) {
            TwelveDataRealTimePrice res = await _twelveDataClient.GetRealTimePriceAsync(symbol);
            return res;
        }

        public async Task<TwelveDataQuote> GetSymbolQuoteAsync(GetQuoteRequest req) {
            TwelveDataQuote res = await _twelveDataClient.GetTimeSeriesQuoteAsync(req.Symbol, req.Interval);
            return res;
        }

        public async Task<TwelveDataTimeSeries> GetTimeSeriesAsync(GetTimeSeriesRequest req) {
            TwelveDataTimeSeries res = await _twelveDataClient.GetTimeSeriesAsync(req.Symbol, req.Interval);
            return res;
        }
    }
}