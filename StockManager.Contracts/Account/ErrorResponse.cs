namespace StockManager.Contracts.Account
{
    public record ErrorResponse
    (
        string? ErrorCode,
        string? Description
    );
}