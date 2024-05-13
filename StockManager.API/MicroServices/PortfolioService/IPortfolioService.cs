using StockManager.API.Models;
using StockManager.API.Database;
using StockManager.Contracts.Portfolio;


namespace StockManager.API.MicroServices.PortfolioService
{
    public interface IPortfolioService
    {
        DatabaseResult<Portfolio> CreatePortfolio(CreatePortfolioRequest req);
        DatabaseResult<Portfolio> GetPortfolio(Guid id);
        DatabaseResult<Portfolio> UpdatePortfolio(UpdatePortfolioRequest req);
        DatabaseResult<Portfolio> DeletePortfolio(Guid id);
    }
}