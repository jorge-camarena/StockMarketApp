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

                if (jsonReponse.Symbol != string.Empty) {
                    TwelveDataQuote quote = new TwelveDataQuote() {
                        Symbol = jsonReponse.Symbol,
                        Name = jsonReponse.Name,
                        Exchange = jsonReponse.Exchange,
                        MicCode = jsonReponse.MicCode,
                        Currency = jsonReponse.Currency,
                        DateTime = jsonReponse.DateTime,
                        TimeStamp = Convert.ToInt64(jsonReponse.TimeStamp),
                        Open = Convert.ToDouble(jsonReponse.Open),
                        High = Convert.ToDouble(jsonReponse.High),
                        Low = Convert.ToDouble(jsonReponse.Low),
                        Close = Convert.ToDouble(jsonReponse.Close),
                        Volume = Convert.ToInt64(jsonReponse.Volume),
                        PreviousClose = Convert.ToDouble(jsonReponse.PreviousClose),
                        Change = Convert.ToDouble(jsonReponse.Change),
                        PercentChange = Convert.ToDouble(jsonReponse.PercentChange),
                        AverageVolume = Convert.ToInt64(jsonReponse.AverageVolume),
                        IsMarketOpen = jsonReponse.IsMarketOpen,
                        FiftyTwoWeekLow = Convert.ToDouble(jsonReponse.FiftyTwoWeek.Low),
                        FiftyTwoWeekHigh = Convert.ToDouble(jsonReponse.FiftyTwoWeek.High),
                        FiftyTwoWeekLowChange = Convert.ToDouble(jsonReponse.FiftyTwoWeek.LowChange),
                        FiftyTwoWeekHighChange = Convert.ToDouble(jsonReponse.FiftyTwoWeek.HighChange),
                        FiftyTwoWeekLowChangePercent = Convert.ToDouble(jsonReponse.FiftyTwoWeek.LowChangePercent),
                        FiftyTwoWeekHighChangePercent = Convert.ToDouble(jsonReponse.FiftyTwoWeek.HighChangePercent),
                        FiftyTwoWeekRange = jsonReponse.FiftyTwoWeek.Range,
                        ResponseStatus = ResponseStatus.Ok,
                        ResponseMessage = "Success"
                    };
                    return quote;
                } else {
                    TwelveDataQuote quote = new TwelveDataQuote() {
                        ResponseStatus = ResponseStatus.TwelveDataAPIError,
                        ResponseMessage = "Invalid symbol entered"
                    };
                    return quote;
                }
            } catch (Exception e)
            {
                TwelveDataQuote quote = new TwelveDataQuote() {
                    ResponseStatus = ResponseStatus.TwelveDataDotNetError,
                    ResponseMessage = e.Message
                };
                return quote;
            }
        }

        public async Task<TwelveDataTimeSeries> GetTimeSeriesAsync(string symbol, string interval = "1day") {
            try 
            {
                string url = $"https://api.twelvedata.com/time_series?symbol={symbol}&interval={interval}&apikey={_apiKey}";
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
                if (timeSeries.Currency != string.Empty) {
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
        }

        public async Task<TwelveDataRealTimePrice> GetRealTimePriceAsync(string symbol) {
            try
            {
                string url = $"https://api.twelvedata.com/price?symbol={symbol}&apikey={_apiKey}";
                var response = await _httpClient.GetAsync(url);
                var responseString = await response.Content.ReadAsStringAsync();
                var jsonResponse = JsonConvert.DeserializeObject<RealTimePrice> (responseString);

                TwelveDataRealTimePrice realTimePrice = new TwelveDataRealTimePrice();
                if (jsonResponse.Price != string.Empty) {
                    realTimePrice.Price = Convert.ToDouble (jsonResponse.Price);
                    realTimePrice.ResponseStatus = ResponseStatus.Ok;
                    realTimePrice.ResponseMessage = "Success";
                    return realTimePrice;
                } 
                realTimePrice.ResponseStatus = ResponseStatus.TwelveDataAPIError;
                realTimePrice.ResponseMessage = "Invalid symbol.";
                return realTimePrice;
            } catch (Exception e)
            {
                Console.WriteLine("Exception");
                Console.WriteLine(e);
                TwelveDataRealTimePrice realTimePrice = new TwelveDataRealTimePrice() {
                    ResponseStatus = ResponseStatus.TwelveDataDotNetError,
                    ResponseMessage = e.Message
                };
                return realTimePrice;
            }
        }
    }
}