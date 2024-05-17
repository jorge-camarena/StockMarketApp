using Newtonsoft.Json;

namespace StockManager.TwelveDataDotNet.Api.ResponseModels
{
    public class TimeSeriesQuote
    {
        [JsonProperty("symbol")]
        public string Symbol { get; set;}

        [JsonProperty("name")]
        public string Name { get; set;}

        [JsonProperty("exchange")]
        public string Exchange { get; set;}

        [JsonProperty("mic_code")]
        public string MicCode { get; set;}

        [JsonProperty("currency")]
        public string Currency { get; set;}

        [JsonProperty("datetime")]
        public string DateTime { get; set;}

        [JsonProperty("timestamp")]
        public long TimeStamp { get; set;}

        [JsonProperty("open")]
        public string Open { get; set;}

        [JsonProperty("high")]
        public string High { get; set;}

        [JsonProperty("low")]
        public string Low { get; set;}

        [JsonProperty("close")]
        public string Close { get; set;}

        [JsonProperty("volume")]
        public string Volume { get; set;}

        [JsonProperty("previous_close")]
        public string PreviousClose { get; set;}

        [JsonProperty("change")]
        public string Change { get; set;}

        [JsonProperty("percentage_change")]
        public string PercentageChange { get; set;}

        [JsonProperty("average_volume")]
        public string AverageVolume { get; set;}

        [JsonProperty("rolling_1d_change")]
        public string RollingOneDayChange { get; set;}

        [JsonProperty("rolling_7d_change")]
        public string RollingSevenDayChange { get; set;}

        [JsonProperty("rolling_period_change")]
        public string RollingPeriodDayChange { get; set;}

        [JsonProperty("is_market_open")]
        public bool IsMarketOpen { get; set;}

        [JsonProperty("fifty_two_week")]
        public FiftyTwoWeek FiftyTwoWeek { get; set;}

        [JsonProperty("extended_change")]
        public string ExtendedChange { get; set;}

        [JsonProperty("extended_percentage_change")]
        public string ExtendedPercentageChange { get; set;}

        [JsonProperty("extended_price")]
        public string ExtendedPrice { get; set;}

        [JsonProperty("extended_timestamp")]
        public long ExtendedTimeStamp { get; set;} 
    }
    public partial class FiftyTwoWeek
    {
        [JsonProperty("low")]
        public string Low { get; set;}
        
        [JsonProperty("high")]
        public string High { get; set;}

        [JsonProperty("low_change")]
        public string LowChange { get; set;}

        [JsonProperty("high_change")]
        public string HighChange { get; set;}

        [JsonProperty("low_change_percentage")]
        public string LowChangePercentage { get; set;}

        [JsonProperty("high_change_percentage")]
        public string HighChangePercentage { get; set;}

        [JsonProperty("range")]
        public string Range { get; set;}
    }
}