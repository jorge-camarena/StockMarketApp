using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockManager.Contracts.Portfolio
{
    public record CreatePortfolioRequest
    (
        Guid AccountId,
        string PortfolioName,
        string PortfolioType
    );
}