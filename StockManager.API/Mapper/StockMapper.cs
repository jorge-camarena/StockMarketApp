using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StockManager.API.Models;
using StockManager.Contracts.Stock;

namespace StockManager.API.Mapper
{
    public class StockMapper
    {
        public static StockResponse ToResponse(Stock stock) {
            return new StockResponse(
                Id: stock.Id,
                StockSymbol: stock.Symbol,
                AmountInvested: stock.AmountInvested
            );
        }
    }
}