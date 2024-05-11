namespace StockManager.Contracts.Account
{
    public record UpdateAccountRequest
    (
        Guid Id,
        string? Name,
        string? Email,
        string? Password,
        string? AccountType
    );
}