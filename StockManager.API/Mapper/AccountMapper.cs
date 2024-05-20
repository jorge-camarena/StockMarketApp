using StockManager.Contracts.Account;
using StockManager.API.Models;

namespace StockManager.API.Mapper
{
    public class AccountMapper
    {
        public static AccountResponse ToResponse(Account account) {
            return new AccountResponse
            (
                Id: account.Id,
                Name: account.Name,
                Email: account.Email,
                AccountType: account.AccountType,
                CreatedAtDateTime: account.CreatedAtDateTime,
                LastUpdatedDateTime: account.LastUpdatedDateTime
            );
        }
    }
}