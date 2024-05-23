using StockManager.Contracts.SearchSymbolData;
using StockManager.TwelveDataDotNet.Client;
using StockManager.TwelveDataDotNet.Library.ResponseModels;

namespace StockManager.API.MicroServices.SearchSymbolDataService
{
    public class SearchSymbolDataService : ISearchSymbolDataService
    {
        private readonly  TwelveDataClient _twelveDataClient;
        protected readonly IConfiguration _configuration;

        public SearchSymbolDataService(IConfiguration configuration) {
            _configuration = configuration;
            string? apiKey = configuration.GetValue<string>("TwelveDataAPIKey");
            _twelveDataClient = new TwelveDataClient(new HttpClient(), apiKey);
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