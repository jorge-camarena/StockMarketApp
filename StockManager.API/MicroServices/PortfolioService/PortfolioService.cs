using Microsoft.EntityFrameworkCore;
using StockManager.API.Database;
using StockManager.API.Models;
using StockManager.API.ServiceErrors;
using StockManager.Contracts.Portfolio;

namespace StockManager.API.MicroServices.PortfolioService
{
    public class PortfolioService : IPortfolioService
    {
        private readonly DatabaseContext _dbContext;

        public PortfolioService(DatabaseContext dbContext) {
            _dbContext = dbContext;
        }

        public DatabaseResult<Portfolio> CreatePortfolio(CreatePortfolioRequest req) {
            Account? account = _dbContext.Accounts
                .FirstOrDefault(x => x.Id == req.AccountId);
            if (account == null) {
                Error error = PortfolioError.NoAssociatedAccout(req.AccountId);
                var result = DatabaseResult<Portfolio>.Err(error);
                return result;
            }
            try
            {
                Guid portfolioId = Guid.NewGuid();
                Portfolio portfolio = new Portfolio{
                    Id = portfolioId,
                    AccountId = req.AccountId,
                    PortfolioName = req.PortfolioName,
                    PortfolioType = req.PortfolioType,
                    CreatedAtDateTime = DateTime.UtcNow,
                    LastUpdatedAtDateTime = DateTime.UtcNow
                };
                _dbContext.Add(portfolio);
                _dbContext.SaveChanges();
                var result = DatabaseResult<Portfolio>.Ok(portfolio);
                return result;
            } catch(Microsoft.EntityFrameworkCore.DbUpdateException)
            {
                Error error = PortfolioError.EmptyFields();
                var result = DatabaseResult<Portfolio>.Err(error);
                return result;
            } catch (Exception)
            {
                Error error = PortfolioError.UnknownError();
                var  result = DatabaseResult<Portfolio>.Err(error);
                return result;
            }
        }

        public DatabaseResult<Portfolio> GetPortfolio(Guid id) {
            Portfolio? portfolio = _dbContext.Portfolios
                .FirstOrDefault(p => p.Id == id);
            if (portfolio == null) {
                Error error = PortfolioError.NotFound(id);
                var result = DatabaseResult<Portfolio>.Err(error);
                return result;
            } else {
                var result = DatabaseResult<Portfolio>.Ok(portfolio);
                return result;
            }
        }

        public DatabaseResult<Portfolio> UpdatePortfolio(UpdatePortfolioRequest req) {
            Portfolio? portfolio = _dbContext.Portfolios
                .FirstOrDefault(p => p.Id == req.Id);
            if (portfolio == null) {
                Error error = PortfolioError.NotFound(req.Id);
                var result = DatabaseResult<Portfolio>.Err(error);
                return result;
            }
            try 
            {
                portfolio.PortfolioName = req.PortfolioName;
                portfolio.PortfolioType = req.PortfolioType;
                _dbContext.SaveChanges();
                var result = DatabaseResult<Portfolio>.Ok(portfolio);
                return result;
            } catch (Exception)
            {
                Error error = PortfolioError.UnknownError();
                var result = DatabaseResult<Portfolio>.Err(error);
                return result;
            }
        }

        public DatabaseResult<Portfolio> DeletePortfolio(Guid id) {
            Portfolio? portfolio = _dbContext.Portfolios
                .FirstOrDefault(p => p.Id == id);
            if (portfolio == null) {
                Error error = PortfolioError.NotFound(id);
                var result = DatabaseResult<Portfolio>.Err(error);
                return result;
            }
            try 
            {
                using var transaction = _dbContext.Database.BeginTransaction();
                string savePoint = "Delete stocks associated with portfolio";
                try 
                {
                    _dbContext.Stock.Where(x => x.PortfolioId == id).ExecuteDelete();
                    _dbContext.SaveChanges();

                    transaction.CreateSavepoint(savePoint);

                    _dbContext.Remove(portfolio);
                    _dbContext.SaveChanges();

                    transaction.Commit();

                } catch (Exception)
                {
                    transaction.RollbackToSavepoint(savePoint);
                }
                var result = DatabaseResult<Portfolio>.Ok(portfolio);
                return result;

            } catch (Exception)
            {
                Error error = PortfolioError.UnknownError();
                var result = DatabaseResult<Portfolio>.Err(error);
                return result;
            }
        }
    }
}