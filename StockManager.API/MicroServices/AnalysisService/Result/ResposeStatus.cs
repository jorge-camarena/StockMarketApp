namespace StockManager.API.MicroServices.AnalysisService.Result
{
    public enum ResponseStatus
    {
        Ok,
        TwelveDataAPIError,
        TwelveDataDotNetError,
        DatabaseError,
    }
}