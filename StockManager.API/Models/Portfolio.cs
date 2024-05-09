using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockManager.API.Models
{
    public class Portfolio
    {
        public Guid Id { get; set; }
        public Guid AccountId { get; set; }
        public string PortfolioType { get; set; } = string.Empty;
        public DateTime CreatedAtDateTime { get; set; }
        public DateTime LastUpdatedAtDateTime { get; set;}
    }
}