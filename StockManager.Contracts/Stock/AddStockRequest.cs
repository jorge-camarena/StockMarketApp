using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockManager.Contracts.Stock
{
    public record AddStockRequest
    (
        Guid AccountId,
        Guid PortfolioId,
        string Symbol,
        float AmountInvested
    );
}