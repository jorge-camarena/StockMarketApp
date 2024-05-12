using System.Reflection.Metadata.Ecma335;
using Microsoft.EntityFrameworkCore;
using StockManager.API.Database;
using StockManager.API.Models;
using StockManager.API.ServiceErrors;
using StockManager.Contracts.Account;

namespace StockManager.API.MicroServices.AccountService
{
    public class AccountService : IAccountService
    {
        private readonly DatabaseContext _dbContext;
        
        public AccountService(DatabaseContext dbContext) {
            _dbContext = dbContext;
        }

        public DatabaseResult<Account> CreateAccount(CreateAccountRequest req) {
            //TODO: Hash Password
            string? email = req.Email;
            Account? accountQuery = _dbContext.Accounts.FirstOrDefault(x => x.Email == email);
            if (accountQuery is not null) {
                Error error = AccountError.EmailNotUnique(email);
                var result = new DatabaseResult<Account>(accountQuery, error, true);
                return result;
            }
            try 
            {
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
                var result = new DatabaseResult<Account>(account, null, false);
                return result;
            } catch (Microsoft.EntityFrameworkCore.DbUpdateException) 
            {
                Error error = AccountError.EmptyFields();
                var result = new DatabaseResult<Account>(null, error, true);
                return result;
            } finally
            {
                Error error = AccountError.UnknownError();
                var result = new DatabaseResult<Account>(null, error, true);
            }       
        }

        public DatabaseResult<Account> GetAccount(Guid id) {
            Account? account = _dbContext.Accounts
                .FirstOrDefault(a => a.Id == id);
            if (account == null) {
                Error error = AccountError.NotFound(id);
                var result = new DatabaseResult<Account>(null, error, true);
                return result;
            } else {
                var result = new DatabaseResult<Account>(account, null, false);
                return result;
            }
        }

        public DatabaseResult<Account> UpdateAccount(UpdateAccountRequest req) {
            var accountToUpdate = _dbContext.Accounts
                .FirstOrDefault(a => a.Id == req.Id);
            if (accountToUpdate == null) {
                Error error = AccountError.NotFound(req.Id);
                var result = new DatabaseResult<Account>(null, error, true);
                return result;
            }
            try 
            {
                accountToUpdate.Name = req.Name;
                accountToUpdate.Email = req.Email;
                accountToUpdate.Password = req.Password;
                accountToUpdate.AccountType = req.AccountType;
                _dbContext.SaveChanges();
                var result = new DatabaseResult<Account>(accountToUpdate, null, false);
                return result;
            } catch (Exception)
            {
                Error error = AccountError.UnknownError();
                var result = new DatabaseResult<Account>(null, error, true);
                return result;
            }

        }

        public DatabaseResult<Account> DeleteAccount(Guid id) {
            var account = _dbContext.Accounts
                .FirstOrDefault(a => a.Id == id);
            if (account == null) {
                Error error = AccountError.NotFound(id);
                var result = new DatabaseResult<Account>(null, error, true);
                return result;
            }
            try 
            {
                _dbContext.Remove(account);
                _dbContext.SaveChanges();
                var result = new DatabaseResult<Account>(account, null, false);
                return result;
            } catch (Exception)
            {
                Error error = AccountError.UnknownError();
                var result = new DatabaseResult<Account>(null, error, true);
                return result;
            }
        }        
    }
}