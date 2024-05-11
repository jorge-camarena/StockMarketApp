using Microsoft.EntityFrameworkCore;
using StockManager.API.Database;
using StockManager.API.Models;
using StockManager.Contracts.Account;

namespace StockManager.API.MicroServices.AccountService
{
    public class AccountService : IAccountService
    {
        private readonly DatabaseContext _dbContext;
        
        public AccountService(DatabaseContext dbContext) {
            _dbContext = dbContext;
        }

        public Account CreateAccount(CreateAccountRequest req) {
            //TODO: Hash Password
            Guid accountId = Guid.NewGuid();
            Account account = new Account{
                Id = accountId,
                Name = req.Name,
                Email =  req.Email,
                Password = req.Password,
                AccountType = req.AccountType,
                CreatedAtDateTime = DateTime.UtcNow,
                LastUpdatedDateTime = DateTime.UtcNow,
            };
            _dbContext.Add(account);
            _dbContext.SaveChanges();
            return account;
        }

        public Account GetAccount(Guid id) {
            Account account = _dbContext.Accounts
                .First(a => a.Id == id);
            return account;
        }

        public Account UpdateAccount(UpdateAccountRequest req) {
            var accountToUpdate = _dbContext.Accounts
                .First(a => a.Id == req.Id);
            accountToUpdate.Name = req.Name;
            accountToUpdate.Email = req.Email;
            accountToUpdate.Password = req.Password;
            accountToUpdate.AccountType = req.AccountType;
            _dbContext.SaveChanges();
            return accountToUpdate;
        }

        public Account DeleteAccount(Guid id) {
            var account = _dbContext.Accounts
                .First(a => a.Id == id);
            _dbContext.Remove(account);
            _dbContext.SaveChanges();
            return account;

        }        
    }
}