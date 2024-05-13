using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockManager.Contracts.Portfolio
{
    public record UpdatePortfolioRequest
    (
        Guid Id,
        string PortfolioName,
        string PortfolioType
    );
}