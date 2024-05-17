namespace StockManager.TwelveDataDotNet.Library.ResponseModels
{
    public class TwelveDataQuote
    {
        public string Symbol { get; set;}

        public string Name { get; set;}

        public string Exchange { get; set;}

        public string MicCode { get; set;}

        public string Currency { get; set;}

        public string DateTime { get; set;}

        public long TimeStamp { get; set;}

        public double Open { get; set;}

        public double High { get; set;}

        public double Low { get; set;}

        public double Close { get; set;}

        public long Volume { get; set;}

        public double PreviousClose { get; set;}

        public double Change { get; set;}

        public double PercentageChange { get; set;}

        public double AverageVolume { get; set;}

        public double RollingOneDayChange { get; set;}

        public double RollingSevenDayChange { get; set;}

        public double RollingPeriodDayChange { get; set;}

        public bool IsMarketOpen { get; set;}

        public double FiftyTwoWeekLow { get; set;}

        public double FiftyTwoWeekHigh { get; set;}

        public double FiftyTwoWeekLowChange { get; set;}

        public double FiftyTwoWeekHighChange { get; set;}

        public double FiftyTwoWeekLowChangePercentage { get; set;}

        public double FiftyTwoWeekHighChangePercentage { get; set;}

        public string FiftyTwoWeekRange { get; set;}

        public double ExtendedChange { get; set;}

        public double ExtendedPercentageChange { get; set;}

        public double ExtendedPrice { get; set;}

        public long ExtendedTimeStamp { get; set;}

        public ResponseStatus ResponseStatus { get; set;}

        public string ResponseMessage { get; set;} = string.Empty;
    }
}