using StockManager.TwelveDataDotNet.Library.ResponseModels;

namespace StockManager.API.MicroServices.AnalysisService.Result
{
    public class StockAnalysisResult
    {
        public string Symbol { get; set; } = string.Empty;

        public string Interval { get; set; } = string.Empty;

        public string Currency { get; set; } = string.Empty;

        public string Exchange { get; set; } = string.Empty;

        public string Type { get; set; } = string.Empty;
        
        public List<StockAnalysis> Values { get; set; } = new List<StockAnalysis>();

        public double AverageIntervalGrowth { get; set; }

        public double AveragePercentIntervalGrowth { get; set; }

        public double OverallTimePeriodGrowth { get; set; }

        public double OverallPercentTimePeriodGrowth { get; set; }

        public double HighestValue { get; set; }

        public double LowestValue { get; set; }

        public double Range { get; set; }

        public double AverageValue { get; set; }

        public double MedianValue { get; set; }

        public ResponseStatus ResponseStatus { get; set; }

        public string ResponseMessage { get; set; } = string.Empty;

    }

    public class StockAnalysis 
    {
        public string DateTime { get; set; } = string.Empty;

        public double Close { get; set; }

        public double PreviousClose { get; set; }

        public double Change { get; set; }

        public double PercentChange { get; set; }

    }
}