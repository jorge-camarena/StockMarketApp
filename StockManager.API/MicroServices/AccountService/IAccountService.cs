using StockManager.API.Database;
using StockManager.API.Models;
using StockManager.Contracts.Account;

namespace StockManager.API.MicroServices.AccountService
{
    public interface IAccountService
    {
        DatabaseResult<Account> CreateAccount(CreateAccountRequest req);
        DatabaseResult<Account> GetAccount(Guid id);
        DatabaseResult<Account> UpdateAccount(UpdateAccountRequest req);
        DatabaseResult<Account> DeleteAccount(Guid id);

        
    }
}