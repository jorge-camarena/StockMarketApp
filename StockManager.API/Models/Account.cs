using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockManager.API.Models
{
    public class Account
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? AccountType { get; set; }
        public DateTime CreatedAtDateTime { get; set; }
        public DateTime LastUpdatedDateTime { get; set; }
    }
}