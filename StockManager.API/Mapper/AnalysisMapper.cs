using StockManager.API.MicroServices.AnalysisService.Result;
using StockManager.Contracts.Analysis;
namespace StockManager.API.Mapper
{
    public class AnalysisMapper
    {
        public static GetStockAnalysisResponse StockToResponse(StockAnalysisResult result) {
            List<StockAnalysisObject> valuesList = new List<StockAnalysisObject>();
            foreach (var val in result.Values) {
                StockAnalysisObject resObject = new StockAnalysisObject(
                    Datetime: val.DateTime,
                    Close: val.Close,
                    PreviousClose: val.PreviousClose,
                    Change: val.Change,
                    PercentChange: val.PercentChange
                );
                valuesList.Add(resObject);
            }

            return new GetStockAnalysisResponse(
                Symbol: result.Symbol,
                Interval: result.Interval,
                Currency: result.Currency,
                Exchange: result.Exchange,
                Type: result.Type,
                Values: valuesList,
                AverageIntervalGrowth: result.AverageIntervalGrowth,
                AveragePercentIntervalGrowth: result.AveragePercentIntervalGrowth,
                OverallTimePeriodGrowth: result.OverallTimePeriodGrowth,
                OverallPercentTimePeriodGrowth: result.OverallPercentTimePeriodGrowth,
                HighestValue: result.HighestValue,
                LowestValue: result.LowestValue,
                Range: result.Range,
                AverageValue: result.AverageValue,
                MedianValue: result.MedianValue
            );
        }

        public static GetPortfolioAnalysisResponse PortfolioToResponse(PortfolioAnalysisResult result) {
            List<GetStockAnalysisResponse> stockAnalysisResponseList = new List<GetStockAnalysisResponse>();
            foreach(var val in result.Stocks) {
                GetStockAnalysisResponse stockAnalysisResponseObj = StockToResponse(val);
                stockAnalysisResponseList.Add(stockAnalysisResponseObj);
            }
            return new GetPortfolioAnalysisResponse(
                PortfolioId: result.PortfolioId,
                PortfolioName: result.PortfolioName,
                PortfolioType: result.PortfolioType,
                Stocks: stockAnalysisResponseList
            );
        }
    }
}