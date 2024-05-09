namespace StockManager.Contracts.Account
{
    public record UpdateAccountRequest
    (
        Guid id,
        string? Name,
        string? Email,
        string? Password,
        DateTime LastUpdatedDateTime
    );
}