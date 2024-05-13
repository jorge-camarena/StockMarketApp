namespace StockManager.Contracts.Error
{
    public record ErrorResponse
    (
        string? ErrorCode,
        string? Description
    );
}