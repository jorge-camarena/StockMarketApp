namespace StockManager.Contracts.Account

{
    public record AccountResponse
    (
        Guid Id,
        string? Name,
        string? Email,
        string? AccountType,
        DateTime CreatedAtDateTime,
        DateTime LastUpdatedDateTime
    );
}