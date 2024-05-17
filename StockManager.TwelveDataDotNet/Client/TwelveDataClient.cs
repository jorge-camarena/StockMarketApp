using System.Linq.Expressions;
using Newtonsoft.Json;
using StockManager.TwelveDataDotNet.Api.ResponseModels;
using StockManager.TwelveDataDotNet.Library.ResponseModels;

namespace StockManager.TwelveDataDotNet.Client
{
    public class TwelveDataClient
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;

        public TwelveDataClient(HttpClient httpClient, string apiKey) {
            _httpClient = httpClient;
            _apiKey = apiKey;
        }

        public async Task<TwelveDataQuote> GetTimeSeriesQuoteAsync(string symbol, string interval = "1day") {
            try
            {
                string url = $"https://api.twelvedata.com/quote?symbol={symbol}&interval={interval}&apikey={_apiKey}";
                var response = await _httpClient.GetAsync(url);
                string responseString = await response.Content.ReadAsStringAsync();
                var jsonReponse = JsonConvert.DeserializeObject<TimeSeriesQuote>(responseString);

                TwelveDataQuote quote = new TwelveDataQuote() {
                    Symbol = jsonReponse.Symbol,
                    Name = jsonReponse.Name,
                    Exchange = jsonReponse.Exchange,
                    MicCode = jsonReponse.MicCode,
                    Currency = jsonReponse.Currency,
                    DateTime = jsonReponse.DateTime,
                    TimeStamp = jsonReponse.TimeStamp,
                    Open = Convert.ToDouble(jsonReponse.Open),
                    High = Convert.ToDouble(jsonReponse.High),
                    Low = Convert.ToDouble(jsonReponse.Low),
                    Close = Convert.ToDouble(jsonReponse.Close),
                    Volume = Convert.ToInt32(jsonReponse.Volume),
                    PreviousClose = Convert.ToDouble(jsonReponse.PreviousClose),
                    Change = Convert.ToDouble(jsonReponse.Change),
                    PercentageChange = Convert.ToDouble(jsonReponse.PercentageChange),
                    AverageVolume = Convert.ToDouble(jsonReponse.AverageVolume),
                    RollingOneDayChange = Convert.ToDouble(jsonReponse.RollingOneDayChange),
                    RollingSevenDayChange = Convert.ToDouble(jsonReponse.RollingSevenDayChange),
                    RollingPeriodDayChange = Convert.ToDouble(jsonReponse.RollingPeriodDayChange),
                    IsMarketOpen = jsonReponse.IsMarketOpen,
                    FiftyTwoWeekLow = Convert.ToDouble(jsonReponse.FiftyTwoWeek.Low),
                    FiftyTwoWeekHigh = Convert.ToDouble(jsonReponse.FiftyTwoWeek.High),
                    FiftyTwoWeekLowChange = Convert.ToDouble(jsonReponse.FiftyTwoWeek.LowChange),
                    FiftyTwoWeekHighChange = Convert.ToDouble(jsonReponse.FiftyTwoWeek.HighChange),
                    FiftyTwoWeekLowChangePercentage = Convert.ToDouble(jsonReponse.FiftyTwoWeek.LowChangePercentage),
                    FiftyTwoWeekHighChangePercentage = Convert.ToDouble(jsonReponse.FiftyTwoWeek.HighChangePercentage),
                    FiftyTwoWeekRange = jsonReponse.FiftyTwoWeek.Range,
                    ExtendedChange = Convert.ToDouble(jsonReponse.ExtendedChange),
                    ExtendedPercentageChange = Convert.ToDouble(jsonReponse.ExtendedPercentageChange),
                    ExtendedPrice = Convert.ToDouble(jsonReponse.ExtendedPrice),
                    ExtendedTimeStamp = jsonReponse.ExtendedTimeStamp
                };
                if (quote.Symbol != null) {
                    quote.ResponseStatus = ResponseStatus.Ok;
                    quote.ResponseMessage = "Success";
                    return quote;
                }
                quote.ResponseStatus = ResponseStatus.TwelveDataAPIError;
                quote.ResponseMessage = "Invalid symbol entered";
                return quote;

            } catch (Exception e)
            {
                TwelveDataQuote quote = new TwelveDataQuote() {
                    ResponseStatus = ResponseStatus.TwelveDataDotNetError,
                    ResponseMessage = e.Message
                };
                return quote;
            }
        }

        public async Task<TwelveDataTimeSeries> GetTimeSeries(string symbol, string interval = "1day") {
            try 
            {
                string url = $"https://api.twelvedata.com/time_series?symbol={symbol}&interval={interval}&apikey=your_api_key";
                var response = await _httpClient.GetAsync(url);
                var responseString = await response.Content.ReadAsStringAsync();
                var jsonReponse = JsonConvert.DeserializeObject<TimeSeriesResponse>(responseString);

                TwelveDataTimeSeries timeSeries = new TwelveDataTimeSeries() {
                    Symbol = jsonReponse.Meta.Symbol,
                    Interval = jsonReponse.Meta.Interval,
                    Currency = jsonReponse.Meta.Currency,
                    ExchangeTimezone = jsonReponse.Meta.ExchangeTimezone,
                    Exchange = jsonReponse.Meta.Exchange,
                    MicCode = jsonReponse.Meta.MicCode,
                    Type = jsonReponse.Meta.Type,
                    Status = jsonReponse.Status,
                };
                foreach (var ts_value in jsonReponse.Value) 
                {
                    TwelveDataTSValues td_values = new TwelveDataTSValues() {
                        DateTime = ts_value.DateTime,
                        Open = Convert.ToDouble(ts_value.Open),
                        High = Convert.ToDouble(ts_value.High),
                        Low = Convert.ToDouble(ts_value.Low),
                        Close = Convert.ToDouble(ts_value.Close),
                        Volume = Convert.ToInt32(ts_value.Volume),
                    };
                    timeSeries.Values.Add(td_values);
                }
                if (timeSeries.Symbol != null) {
                    timeSeries.ResponseStatus = ResponseStatus.Ok;
                    timeSeries.ResponseMessage = "Success";
                    return timeSeries;
                }
                timeSeries.ResponseStatus = ResponseStatus.TwelveDataAPIError;
                timeSeries.ResponseMessage = "Invalid symbol or interval";
                return timeSeries;
            }
            catch (Exception e) 
            {
                TwelveDataTimeSeries timeSeries = new TwelveDataTimeSeries() {
                ResponseStatus = ResponseStatus.TwelveDataDotNetError,
                ResponseMessage = e.Message
                };

                return timeSeries;
            }






            throw new NotImplementedException();

        }
    }
}