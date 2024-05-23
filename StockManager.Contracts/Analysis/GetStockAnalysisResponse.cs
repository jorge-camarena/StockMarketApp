namespace StockManager.Contracts.Analysis
{
    public record GetStockAnalysisResponse
    (
        string Symbol,
        string Interval,
        string Currency,
        string Exchange,
        string Type,
        List<StockAnalysisObject> Values,
        double AverageIntervalGrowth,
        double AveragePercentIntervalGrowth,
        double OverallTimePeriodGrowth,
        double OverallPercentTimePeriodGrowth,
        double HighestValue,
        double LowestValue,
        double Range,
        double AverageValue,
        double MedianValue
    );

    public record StockAnalysisObject 
    (
        string Datetime,
        double Close,
        double PreviousClose,
        double Change,
        double PercentChange
    );
}