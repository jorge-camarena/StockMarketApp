using StockManager.TwelveDataDotNet.Api.ResponseModels;
using StockManager.TwelveDataDotNet.Library.ResponseModels;

namespace StockManager.TwelveDataDotNet.Library.ResponseModels
{
    public class TwelveDataTimeSeries
    {
        public string Symbol { get; set; } = string.Empty;

        public string Interval { get; set; } = string.Empty;

        public string Currency { get; set; } = string.Empty;

        public string ExchangeTimezone { get; set; } = string.Empty;

        public string Exchange { get; set; } = string.Empty;

        public string MicCode { get; set; } = string.Empty;
        
        public string Type { get; set; } = string.Empty;

        public List<TwelveDataTSValues> Values { get; set; } = new List<TwelveDataTSValues>();

        public string Status { get; set; } = string.Empty;

        public ResponseStatus ResponseStatus { get; set; }

        public string ResponseMessage { get; set; } = string.Empty;
    }

    public partial class TwelveDataTSValues 
    {
        public string DateTime { get; set; } = string.Empty;

        public double Open { get; set; }

        public double High { get; set; }

        public double Low { get; set; }

        public double Close { get; set; }

        public long Volume { get; set; }

        public double PreviousClose { get; set; }
    }
}