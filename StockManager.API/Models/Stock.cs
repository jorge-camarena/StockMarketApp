using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockManager.API.Models
{
    //TODO: figure if add Pricepershare
    public class Stock
    {
        public Guid Id { get; set; }
        public Guid PortfolioId { get; set; }
        public string Symbol { get; set; } = string.Empty;
        public float AmountInvested { get; set; }
        public float SharesOwned { get; set; }
        public DateTime AddedToPortfolioDateTime { get; set; }
    }
}