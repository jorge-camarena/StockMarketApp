using StockManager.API.Models;

namespace StockManager.API.MicroServices.AccountService
{
    public interface IAccountService
    {
        void CreateAccount(Account account);
        Account GetAccount(Guid id);
        Account UpdateAccount(Guid id, Account account);
        Account DeleteAccount(Guid id);

        
    }
}