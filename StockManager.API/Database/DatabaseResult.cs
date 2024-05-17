using StockManager.API.ServiceErrors;

namespace StockManager.API.Database
{
    public class DatabaseResult<T>
    {
        public T? Value;
        public Error? Error;
        public bool _success;

        public DatabaseResult(T? value, Error? error, bool success) {
            Value = value;
            Error = error;
            _success = success;
        }

        public static DatabaseResult<T> Ok(T v) {
            return new DatabaseResult<T>(v, default, true);
        }
        public static DatabaseResult<T> Err(Error e) {
            return new DatabaseResult<T>(default(T), e, false);
        }
    }
}