using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace StockManager.TwelveDataDotNet.Api.ResponseModels
{
    public class TimeSeriesResponse
    {
        [JsonProperty("meta")]
        public SymbolMetaData Meta { get; set; } = new SymbolMetaData();

        [JsonProperty("value")]
        public List<TimeSeriesValues> Value { get; set; } = new List<TimeSeriesValues>();

        [JsonProperty("status")]
        public string Status { get; set; } = string.Empty;
    }
    public partial class TimeSeriesValues 
    {
        [JsonProperty("datime")]
        public string DateTime { get; set; } = string.Empty;

        [JsonProperty("open")]
        public string Open { get; set; } = string.Empty;

        [JsonProperty("high")]
        public string High { get; set; } = string.Empty;

        [JsonProperty("low")]
        public string Low { get; set; } = string.Empty;

        [JsonProperty("close")]
        public string Close { get; set; } = string.Empty;

        [JsonProperty("volume")]
        public string Volume { get; set; } = string.Empty;
    }

    public partial class SymbolMetaData 
    {
        [JsonProperty("symbol")]
        public string Symbol { get; set; } = string.Empty;
        
        [JsonProperty("interval")]
        public string Interval { get; set; } = string.Empty;

        [JsonProperty("currency")]
        public string Currency { get; set; } = string.Empty;

        [JsonProperty("exchange_timezone")]
        public string ExchangeTimezone { get; set; } = string.Empty;

        [JsonProperty("exchange")]
        public string Exchange { get; set; } = string.Empty;

        [JsonProperty("mic_code")]
        public string MicCode { get; set; } = string.Empty;

        [JsonProperty("type")]
        public string Type { get; set; } = string.Empty;
    }
}