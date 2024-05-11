using Microsoft.EntityFrameworkCore;
using StockManager.API.Models;

namespace StockManager.API.Database
{
    public class DatabaseContext : DbContext
    {
        // protected readonly IConfiguration Configuration;
        // public DatabaseContext(IConfiguration configuration)
        // {
        //     Configuration = configuration;
        // }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseNpgsql(Configuration.GetConnectionString("StockAppAPIDatabase"));
            optionsBuilder.UseNpgsql("Host=localhost; Database=stock_app_db; Username=root; Password=mysecretpassword");
        }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<Portfolio> Portfolios { get; set; }
        public DbSet<Stock> Stock { get; set; }
        public DbSet<Transactions> Transactions { get; set; }



    }
}