using StockManager.API.Database;
using StockManager.API.Models;
using StockManager.API.ServiceErrors;
using StockManager.Contracts.Account;
using BCrypt.Net;
using Microsoft.EntityFrameworkCore;

namespace StockManager.API.MicroServices.AccountService
{
    public class AccountService : IAccountService
    {
        private readonly DatabaseContext _dbContext;
        
        public AccountService(DatabaseContext dbContext) {
            _dbContext = dbContext;
        }

        public DatabaseResult<Account> CreateAccount(CreateAccountRequest req) {
            string? email = req.Email;
            Account? accountQuery = _dbContext.Accounts.FirstOrDefault(x => x.Email == email);
            if (accountQuery is not null) {
                Error error = AccountError.EmailNotUnique(email);
                var result = DatabaseResult<Account>.Err(error);
                return result;
            }
            try 
            {
                Guid accountId = Guid.NewGuid();
                var passwordHash = BCrypt.Net.BCrypt.HashPassword(req.Password);
                Account account = new Account{
                    Id = accountId,
                    Name = req.Name,
                    Email =  req.Email,
                    Password = passwordHash,
                    AccountType = req.AccountType,
                    CreatedAtDateTime = DateTime.UtcNow,
                    LastUpdatedDateTime = DateTime.UtcNow,
                };
                _dbContext.Add(account);
                _dbContext.SaveChanges();
                var result = DatabaseResult<Account>.Ok(account);
                return result;
            } catch (Microsoft.EntityFrameworkCore.DbUpdateException) 
            {
                Error error = AccountError.EmptyFields();
                var result = DatabaseResult<Account>.Err(error);
                return result;
            } catch (Exception)
            {
                Error error = AccountError.UnknownError();
                var result = DatabaseResult<Account>.Err(error);
                return result;
            }       
        }

        public DatabaseResult<Account> GetAccount(Guid id) {
            Account? account = _dbContext.Accounts
                .FirstOrDefault(a => a.Id == id);
            if (account == null) {
                Error error = AccountError.NotFound(id);
                var result = DatabaseResult<Account>.Err(error);
                return result;
            } else {
                var result = DatabaseResult<Account>.Ok(account);
                return result;
            }
        }

        public DatabaseResult<Account> UpdateAccount(UpdateAccountRequest req) {
            var accountToUpdate = _dbContext.Accounts
                .FirstOrDefault(a => a.Id == req.Id);
            if (accountToUpdate == null) {
                Error error = AccountError.NotFound(req.Id);
                var result = DatabaseResult<Account>.Err(error);
                return result;
            }
            try 
            {
                accountToUpdate.Name = req.Name;
                accountToUpdate.Email = req.Email;
                accountToUpdate.Password = req.Password;
                accountToUpdate.AccountType = req.AccountType;
                _dbContext.SaveChanges();
                var result = DatabaseResult<Account>.Ok(accountToUpdate);
                return result;
            } catch (Exception)
            {
                Error error = AccountError.UnknownError();
                var result = DatabaseResult<Account>.Err(error);
                return result;
            }
        }

        public DatabaseResult<Account> DeleteAccount(Guid id) {
            var account = _dbContext.Accounts
                .FirstOrDefault(a => a.Id == id);
            if (account == null) {
                Error error = AccountError.NotFound(id);
                var result = DatabaseResult<Account>.Err(error);
                return result;
            }
            try 
            {
                
                using var transaction = _dbContext.Database.BeginTransaction();
                string savePoint1 = "Delete stocks associated with account";
                string savePoint2 = "Delete portfolio associated with account";
                try 
                {
                    _dbContext.Stock.Where(x => x.AccountId == id).ExecuteDelete();
                    _dbContext.SaveChanges();

                    transaction.CreateSavepoint(savePoint1);

                    _dbContext.Portfolios.Where(x => x.AccountId == id).ExecuteDelete();
                    _dbContext.SaveChanges();

                    transaction.CreateSavepoint(savePoint2);
                    _dbContext.Remove(account);
                    _dbContext.SaveChanges();
                    transaction.Commit();
                } catch (Exception)
                {
                    transaction.RollbackToSavepoint(savePoint1);
                    transaction.RollbackToSavepoint(savePoint2);
                }
                var result = DatabaseResult<Account>.Ok(account);
                return result;
            } catch (Exception)
            {
                Error error = AccountError.UnknownError();
                var result = DatabaseResult<Account>.Err(error);
                return result;
            }
        }        
    }
}