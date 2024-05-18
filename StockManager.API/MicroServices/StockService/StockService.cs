using StockManager.API.Database;
using StockManager.API.Models;
using StockManager.API.ServiceErrors;
using StockManager.Contracts.Stock;
using StockManager.TwelveDataDotNet.Client;

namespace StockManager.API.MicroServices.StockService
{
    public class StockService : IStockService
    {
        private readonly DatabaseContext _dbContext;
        private readonly TwelveDataClient _twelveDataClient;

        public StockService(DatabaseContext dbContext) {
            _dbContext = dbContext;
            _twelveDataClient = new TwelveDataClient(new HttpClient(), "f6b555954b7d4b8a80fb2571e8a2f223");
            
        }

        public DatabaseResult<Stock> AddStock(AddStockRequest req) {
            Portfolio? portfolio = _dbContext.Portfolios
                .FirstOrDefault(p => p.Id == req.PortfolioId);
            if (portfolio == null) {
                Error error = StockError.NoAssociatedPortfolio(req.PortfolioId);
                var result = new DatabaseResult<Stock>(null, error, true);
                return result;
            }
            try
            {
                Guid stockId = Guid.NewGuid();
                Stock stock = new Stock {
                    Id = stockId,
                    PortfolioId = req.PortfolioId,
                    Symbol = req.Symbol,
                    AmountInvested = req.AmountInvested,
                    SharesOwned = 100,
                    AddedToPortfolioDateTime = DateTime.UtcNow
                };
                _dbContext.Add(stock);
                _dbContext.SaveChanges();
                //TODO: add transaction
                var result = new DatabaseResult<Stock>(stock, null, false);
                return result;
            }
            catch (Microsoft.EntityFrameworkCore.DbUpdateException)
            {
                Error error = StockError.EmptyFields();
                var result = new DatabaseResult<Stock>(null, error, true) ;
                return result; 
            }
            finally
            {
                Error error = StockError.UnknownError();
                var result = new DatabaseResult<Stock>(null, error, false);
            }
        }
        public DatabaseResult<Stock> GetStock(Guid id) {
            Stock? stock = _dbContext.Stock
                .FirstOrDefault(x => x.Id == id); 
            if (stock == null) {
                Error error = StockError.NotFound(id);
                var result = new DatabaseResult<Stock>(null, error, false);
                return result;
            } else {
                var result = new DatabaseResult<Stock>(stock, null, false);
                return result;
            }
        }
        public DatabaseResult<Stock> DeleteStock(Guid id) {
            Stock? stock = _dbContext.Stock
                .FirstOrDefault(x => x.Id == id);
            if (stock == null) {
                Error error = StockError.NotFound(id);
                var result = new DatabaseResult<Stock>(null, error, true);
                return result;
            }
            try 
            {
                _dbContext.Remove(stock);
                _dbContext.SaveChanges();
                var result = new DatabaseResult<Stock>(stock, null, false);
                return result;
            }
            catch (Exception)
            {
                Error error = StockError.UnknownError();
                var result = new DatabaseResult<Stock>(null, error, true);
                return result;
            }
        }
    }
}