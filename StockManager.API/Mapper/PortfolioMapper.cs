using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StockManager.API.Models;
using StockManager.Contracts.Portfolio;

namespace StockManager.API.Mapper
{
    public class PortfolioMapper
    {
        public static PortfolioResponse ToResponse(Portfolio portfolio) {
            return new PortfolioResponse(
                Id: portfolio.Id,
                PortfolioName: portfolio.PortfolioName,
                PortfolioType: portfolio.PortfolioType
            );
        }
    }
}