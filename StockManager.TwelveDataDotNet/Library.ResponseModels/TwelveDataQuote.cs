namespace StockManager.TwelveDataDotNet.Library.ResponseModels
{
    public class TwelveDataQuote
    {
        public string Symbol { get; set;} = string.Empty;

        public string Name { get; set;}  = string.Empty;

        public string Exchange { get; set;} = string.Empty;

        public string MicCode { get; set;} = string.Empty;

        public string Currency { get; set;} = string.Empty;

        public string DateTime { get; set;} = string.Empty;

        public long TimeStamp { get; set;}

        public double Open { get; set;}

        public double High { get; set;}

        public double Low { get; set;}

        public double Close { get; set;}

        public long Volume { get; set;}

        public double PreviousClose { get; set;}

        public double Change { get; set;}

        public double PercentChange { get; set;}

        public long AverageVolume { get; set;}

        public bool IsMarketOpen { get; set;}

        public double FiftyTwoWeekLow { get; set;}

        public double FiftyTwoWeekHigh { get; set;}

        public double FiftyTwoWeekLowChange { get; set;}

        public double FiftyTwoWeekHighChange { get; set;}

        public double FiftyTwoWeekLowChangePercent { get; set;}

        public double FiftyTwoWeekHighChangePercent { get; set;}

        public string FiftyTwoWeekRange { get; set;} = string.Empty;

        public ResponseStatus ResponseStatus { get; set;}

        public string ResponseMessage { get; set;} = string.Empty;
    }
}