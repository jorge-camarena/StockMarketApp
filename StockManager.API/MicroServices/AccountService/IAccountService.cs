using StockManager.API.Models;
using StockManager.Contracts.Account;

namespace StockManager.API.MicroServices.AccountService
{
    public interface IAccountService
    {
        Account CreateAccount(CreateAccountRequest req);
        Account GetAccount(Guid id);
        Account UpdateAccount(UpdateAccountRequest req);
        Account DeleteAccount(Guid id);

        
    }
}