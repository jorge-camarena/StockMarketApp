using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockManager.TwelveDataDotNet.Library.ResponseModels
{
    public class TwelveDataRealTimePrice
    {
        public double Price { get; set; }

        public ResponseStatus ResponseStatus { get; set; }

        public string ResponseMessage { get; set; } = string.Empty;
    }
}