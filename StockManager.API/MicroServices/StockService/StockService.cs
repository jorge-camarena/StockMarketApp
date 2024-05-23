using StockManager.API.Database;
using StockManager.API.Models;
using StockManager.API.ServiceErrors;
using StockManager.Contracts.Stock;
using StockManager.TwelveDataDotNet.Client;
using StockManager.TwelveDataDotNet.Library.ResponseModels;

namespace StockManager.API.MicroServices.StockService
{
    public class StockService : IStockService
    {
        private readonly DatabaseContext _dbContext;
        private readonly TwelveDataClient _twelveDataClient;
        protected readonly IConfiguration _configuration;

        public StockService(DatabaseContext dbContext, IConfiguration configuration) {
            _dbContext = dbContext;
            _configuration = configuration;
            string? apiKey = configuration.GetValue<string>("TwelveDataAPIKey");
            _twelveDataClient = new TwelveDataClient(new HttpClient(), apiKey);
        }

        public async Task<DatabaseResult<Stock>> AddStock(AddStockRequest req) {
            Portfolio? portfolio = _dbContext.Portfolios
                .FirstOrDefault(p => p.Id == req.PortfolioId);
            if (portfolio == null) {
                Error error = StockError.NoAssociatedPortfolio(req.PortfolioId);
                var result = DatabaseResult<Stock>.Err(error);
                return result;
            }
            try
            {
                var twelveDataResult = await _twelveDataClient.GetRealTimePriceAsync(req.Symbol);
                if (twelveDataResult.ResponseStatus != ResponseStatus.Ok) {
                    Error error = StockError.InvalidSymbol(req.Symbol);
                    return DatabaseResult<Stock>.Err(error);
                }
                float sharesOwned = (float)(req.AmountInvested / twelveDataResult.Price);
                Guid stockId = Guid.NewGuid();
                Stock stock = new Stock {
                    Id = stockId,
                    AccountId = req.AccountId,
                    PortfolioId = req.PortfolioId,
                    Symbol = req.Symbol,
                    AmountInvested = req.AmountInvested,
                    SharesOwned = sharesOwned,
                    AddedToPortfolioDateTime = DateTime.UtcNow
                };
                Transactions stockTransaction = new Transactions {
                    Id = Guid.NewGuid(),
                    AccountId = req.AccountId,
                    PortfolioId = req.PortfolioId,
                    StockSymbol = req.Symbol,
                    AmountInvested = req.AmountInvested,
                    SharesBought = sharesOwned,
                    TimeOfTransactionDateTime = DateTime.UtcNow
                };

                using var transaction = _dbContext.Database.BeginTransaction();
                string savePoint = "Stock Model Created";
                try 
                {
                    _dbContext.Add(stock);
                    _dbContext.SaveChanges();
                    
                    transaction.CreateSavepoint(savePoint);
                    _dbContext.Add(stockTransaction);
                    _dbContext.SaveChanges();
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.RollbackToSavepoint(savePoint);
                }
                var result = DatabaseResult<Stock>.Ok(stock);
                return result;
            }
            catch (Microsoft.EntityFrameworkCore.DbUpdateException)
            {
                Error error = StockError.EmptyFields();
                var result = DatabaseResult<Stock>.Err(error) ;
                return result; 
            }
            catch (Exception)
            {
                Error error = StockError.UnknownError();
                var result = DatabaseResult<Stock>.Err(error);
                return result;
            }
        }
        public DatabaseResult<Stock> GetStock(Guid id) {
            Stock? stock = _dbContext.Stock
                .FirstOrDefault(x => x.Id == id);
            if (stock == null) {
                Error error = StockError.NotFound(id);
                var result = DatabaseResult<Stock>.Err(error);
                return result;
            } else {
                var result = DatabaseResult<Stock>.Ok(stock);
                return result;
            }
        }
        public DatabaseResult<Stock> DeleteStock(Guid id) {
            Stock? stock = _dbContext.Stock
                .FirstOrDefault(x => x.Id == id);
            if (stock == null) {
                Error error = StockError.NotFound(id);
                var result = DatabaseResult<Stock>.Err(error);
                return result;
            }
            try 
            {
                _dbContext.Remove(stock);
                _dbContext.SaveChanges();
                var result = DatabaseResult<Stock>.Ok(stock);
                return result;
            }
            catch (Exception)
            {
                Error error = StockError.UnknownError();
                var result = DatabaseResult<Stock>.Err(error);
                return result;
            }
        }
    }
}