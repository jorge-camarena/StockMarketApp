using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StockManager.API.Database;
using StockManager.API.Models;
using StockManager.Contracts.Stock;

namespace StockManager.API.MicroServices.StockService
{
    public interface IStockService
    {
        Task<DatabaseResult<Stock>> AddStock(AddStockRequest req);
        DatabaseResult<Stock> GetStock(Guid id);
        DatabaseResult<Stock> DeleteStock(Guid id);
    }
}