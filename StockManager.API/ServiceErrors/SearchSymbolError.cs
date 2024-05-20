using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockManager.API.ServiceErrors
{
    public class SearchSymbolError
    {
        public static Error NotFound(string description) {
            return Error.NotFound("Symbol.NotFound", description);
        }
        public static Error APIError(string description) {
            return Error.NotFound("Symbol.APIError", description);
        }
    }
}