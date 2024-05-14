using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockManager.Contracts.Stock
{
    //TODO: add PricePerShare
    public record StockResponse
    (
        Guid Id,
        string StockSymbol,
        float AmountInvested
    );
}