using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace StockManager.TwelveDataDotNet.Api.ResponseModels
{
    public class RealTimePrice
    {
        [JsonProperty("price")]
        public string Price { get; set;} = string.Empty;
    }
}