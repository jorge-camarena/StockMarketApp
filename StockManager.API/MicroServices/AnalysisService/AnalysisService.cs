using StockManager.API.Database;
using StockManager.API.MicroServices.AnalysisService.Result;
using StockManager.API.Models;
using StockManager.Contracts.Analysis;
using StockManager.TwelveDataDotNet.Client;
using ResponseStatus = StockManager.API.MicroServices.AnalysisService.Result.ResponseStatus;

namespace StockManager.API.MicroServices.AnalysisService
{
    public class AnalysisService : IAnalysisService
    {
        private readonly DatabaseContext _dbContext;
        private readonly TwelveDataClient _twelveDataClient;
        protected readonly IConfiguration _configuration;

        public AnalysisService(DatabaseContext dbContext, IConfiguration configuration) {
            _dbContext = dbContext;
            _configuration = configuration;
            string? apiKey = configuration.GetValue<string>("TwelveDataAPIKey");
            _twelveDataClient = new TwelveDataClient(new HttpClient(), apiKey);
        }
 
        public async Task<StockAnalysisResult> GetStockAnalysisDataAsync(GetStockAnalysisRequest req) {
            return await GetSingleStockAnalysisAsync(req.Symbol, req.Interval, req.StartDate, req.EndDate);
        }

        public async Task<PortfolioAnalysisResult> GetPortfolioAnalysisDataAsync(GetPortfolioAnalysisRequest req) {
            try 
            {
                Portfolio? portfolio = _dbContext.Portfolios
                    .FirstOrDefault(p => p.Id == req.Id);
                if (portfolio == null) {
                    return new PortfolioAnalysisResult {
                        ResponseStatus = ResponseStatus.DatabaseError,
                        ResponseMessage = $"Portfolio with the id: {req.Id} was not found"
                    };
                }
                var dbResult = _dbContext.Stock.Where(x => x.PortfolioId == req.Id).ToList();

                List<StockAnalysisResult> stocksAnalysisList = new List<StockAnalysisResult>();
                foreach (var val in dbResult) {
                    Console.WriteLine(val.Symbol);
                    StockAnalysisResult stockAnalysisResult = await GetSingleStockAnalysisAsync(val.Symbol, req.Interval, req.StartDate, req.EndDate);
                    if (stockAnalysisResult.ResponseStatus == ResponseStatus.Ok) {
                        stocksAnalysisList.Add(stockAnalysisResult);
                    } else {
                        if (stockAnalysisResult.ResponseStatus == ResponseStatus.TwelveDataAPIError) {
                            return new PortfolioAnalysisResult {
                                ResponseStatus = ResponseStatus.TwelveDataAPIError,
                                ResponseMessage = stockAnalysisResult.ResponseMessage
                            };
                        }
                        else {
                            return new PortfolioAnalysisResult {
                                ResponseStatus = ResponseStatus.TwelveDataDotNetError,
                                ResponseMessage = stockAnalysisResult.ResponseMessage
                            };
                        }
                    }   
                }
                PortfolioAnalysisResult result = new PortfolioAnalysisResult {
                    PortfolioId = req.Id,
                    PortfolioName = portfolio.PortfolioName,
                    PortfolioType = portfolio.PortfolioType,
                    Stocks = stocksAnalysisList,
                    ResponseStatus = ResponseStatus.Ok,
                    ResponseMessage = "Success"
                };
                return result;
            } catch (Exception e) 
            {
                return new PortfolioAnalysisResult {
                    ResponseStatus = ResponseStatus.TwelveDataDotNetError,
                    ResponseMessage = e.Message
                };
            }
        }

        public async Task<StockAnalysisResult> GetSingleStockAnalysisAsync(string symbol, string interval, string startDate, string endDate) {
            var twelveDataResult = await _twelveDataClient.GetTimeSeriesAsync(symbol, interval, previousClose: true ,startDate, endDate);
            try 
            { 
                if (twelveDataResult.ResponseStatus == TwelveDataDotNet.Library.ResponseModels.ResponseStatus.Ok) {
                    List<StockAnalysis> stockAnalysisList = new List<StockAnalysis>();
                    List<float> changeList = new List<float>();
                    List<double> valuesList = new List<double>();
                    foreach (var val in twelveDataResult.Values) {
                        StockAnalysis stockAnalysisObj = new StockAnalysis {
                            DateTime = val.DateTime,
                            Close = val.Close,
                            PreviousClose = val.PreviousClose,
                            Change = ((val.Close - val.PreviousClose) / val.PreviousClose),
                            PercentChange = (((val.Close - val.PreviousClose) / val.PreviousClose)*100)
                        };
                        stockAnalysisList.Add(stockAnalysisObj);
                        valuesList.Add(val.Close);
                        changeList.Add((float)((val.Close - val.PreviousClose) / val.PreviousClose));
                    }
                    var firstIntervalVal = valuesList.Last();
                    var lastIntervalVal = valuesList.First();
                    valuesList.Sort();
                    changeList.Sort();


                    StockAnalysisResult result = new StockAnalysisResult {
                        Symbol = twelveDataResult.Symbol,
                        Interval = twelveDataResult.Interval,
                        Currency = twelveDataResult.Currency,
                        Exchange = twelveDataResult.Exchange,
                        Type = twelveDataResult.Type,
                        Values = stockAnalysisList,
                        AverageIntervalGrowth = changeList.Average(),
                        AveragePercentIntervalGrowth = changeList.Average() * 100,
                        OverallTimePeriodGrowth = (lastIntervalVal - firstIntervalVal) / firstIntervalVal,
                        OverallPercentTimePeriodGrowth = ((lastIntervalVal - firstIntervalVal) / firstIntervalVal)*100,
                        HighestValue = valuesList.Last(),
                        LowestValue = valuesList.First(),
                        Range = valuesList.Last() - valuesList.First(),
                        AverageValue = valuesList.Average(),
                        MedianValue = valuesList[valuesList.Count / 2],
                        ResponseStatus = ResponseStatus.Ok,
                        ResponseMessage = "Success"
                    };
                    return result;
                } else if (twelveDataResult.ResponseStatus == TwelveDataDotNet.Library.ResponseModels.ResponseStatus.Ok) {
                    StockAnalysisResult result = new StockAnalysisResult {
                        ResponseStatus = ResponseStatus.TwelveDataAPIError,
                        ResponseMessage = twelveDataResult.ResponseMessage,
                    };
                    return result;
                } else {
                    Console.WriteLine("flag");
                    StockAnalysisResult result = new StockAnalysisResult {
                        ResponseStatus = ResponseStatus.TwelveDataDotNetError,
                        ResponseMessage = twelveDataResult.ResponseMessage
                    };
                    return result;
                }
            } catch (Exception e) {
                StockAnalysisResult result = new StockAnalysisResult {
                    ResponseStatus = ResponseStatus.TwelveDataDotNetError,
                    ResponseMessage = e.Message
                };
                return result;
            }
        }
    }
}