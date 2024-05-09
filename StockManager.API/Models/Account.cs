using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockManager.API.Models
{
    public class Account
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string AccountType { get; set; } = string.Empty;
        public DateTime CreatedAtDateTime { get; set; }
        public DateTime LastUpdatedDateTime { get; set; }
    }
}