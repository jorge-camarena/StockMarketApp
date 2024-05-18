using Newtonsoft.Json;

namespace StockManager.TwelveDataDotNet.Api.ResponseModels
{
    public class TimeSeriesQuote
    {
        [JsonProperty("symbol")]
        public string Symbol { get; set;} = string.Empty;

        [JsonProperty("name")]
        public string Name { get; set;} = string.Empty;

        [JsonProperty("exchange")]
        public string Exchange { get; set;} = string.Empty;

        [JsonProperty("mic_code")]
        public string MicCode { get; set;} = string.Empty;

        [JsonProperty("currency")]
        public string Currency { get; set;} = string.Empty;

        [JsonProperty("datetime")]
        public string DateTime { get; set;} = string.Empty;

        [JsonProperty("timestamp")]
        public long TimeStamp { get; set;}

        [JsonProperty("open")]
        public string Open { get; set;} = string.Empty;

        [JsonProperty("high")]
        public string High { get; set;} = string.Empty;

        [JsonProperty("low")]
        public string Low { get; set;} = string.Empty;

        [JsonProperty("close")]
        public string Close { get; set;} = string.Empty;

        [JsonProperty("volume")]
        public string Volume { get; set;} = string.Empty;

        [JsonProperty("previous_close")]
        public string PreviousClose { get; set;} = string.Empty;

        [JsonProperty("change")]
        public string Change { get; set;} = string.Empty;

        [JsonProperty("percent_change")]
        public string PercentChange { get; set;} = string.Empty;

        [JsonProperty("average_volume")]
        public string AverageVolume { get; set;} = string.Empty;

        [JsonProperty("is_market_open")]
        public bool IsMarketOpen { get; set;}

        [JsonProperty("fifty_two_week")]
        public FiftyTwoWeek FiftyTwoWeek { get; set;} = new FiftyTwoWeek();
    }
    public partial class FiftyTwoWeek
    {
        [JsonProperty("low")]
        public string Low { get; set;} = string.Empty;
        
        [JsonProperty("high")]
        public string High { get; set;} = string.Empty;

        [JsonProperty("low_change")]
        public string LowChange { get; set;} = string.Empty;

        [JsonProperty("high_change")]
        public string HighChange { get; set;} = string.Empty;

        [JsonProperty("low_change_percent")]
        public string LowChangePercent { get; set;} = string.Empty;

        [JsonProperty("high_change_percent")]
        public string HighChangePercent { get; set;} = string.Empty;

        [JsonProperty("range")]
        public string Range { get; set;} = string.Empty;
    }
}