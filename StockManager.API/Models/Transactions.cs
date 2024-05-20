using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockManager.API.Models
{
    public class Transactions
    {
        public Guid Id { get; set; }
        public Guid AccountId { get; set; }
        public Guid PortfolioId { get; set; }
        public string StockSymbol {get; set; } = string.Empty;
        public float AmountInvested {get; set; }
        public float SharesBought { get; set; }
        public DateTime TimeOfTransactionDateTime { get; set; }
    }
}