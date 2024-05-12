using StockManager.API.ServiceErrors;

namespace StockManager.API.Database
{
    public class DatabaseResult<T>
    {
        public T? Value;
        public Error? Error;
        public bool IsError { get; }

        public DatabaseResult(T? value, Error? error, bool isError) {
            this.Value = value;
            this.Error = error;
            this.IsError = isError;
        }      
    }
}