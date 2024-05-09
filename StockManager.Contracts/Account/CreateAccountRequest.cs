namespace StockManager.Contracts.Account
{
    public record CreateAccountRequest
    (
        string? Name,
        string? Email,
        string? Password,
        string? AccountType
    );
}