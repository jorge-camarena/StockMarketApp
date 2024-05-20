using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StockManager.API.Models;

namespace StockManager.API.ServiceErrors
{
    public class StockError
    {
        public static Error NotFound(Guid id) {
            return Error.NotFound("Stock.NotFound", $"The stock with id: {id} was not found.");
        }
        public static Error NoAssociatedPortfolio(Guid id) {
            return Error.NotFound("Stock.NoAssociatedPortfolioFound", $"No Portfolio with id: {id} found.");
        }
        public static Error EmptyFields() {
            return Error.Failure("Stock.NullOrEmptyFields", "Request contained one or more empty or null fields");
        }
        public static Error UnknownError() {
            return Error.Failure("Stock.UnknownError", "Unknown Error");
        }
        public static Error InvalidSymbol(string symbol) {
            return Error.NotFound("Stock.InvalidSymbol", $"The symbol {symbol} does not exist.");
        }
    }
}