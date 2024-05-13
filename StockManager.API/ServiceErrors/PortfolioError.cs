

namespace StockManager.API.ServiceErrors
{
    public class PortfolioError
    {
        public static Error NotFound(Guid id) {
            return Error.NotFound("Portfolio.NotFound", $"No such portfolio with id:{id} was found.");
        }
        public static Error NoAssociatedAccout(Guid id) {
            return Error.NotFound("Portfolio.NoAssociatedAccountFound", $"No account with the id:{id} was found.");
        }
        public static Error EmptyFields() {
            return Error.Failure("Portfolio.NullOrEmptyFields", "Request contained one or more empty or null fields");
        }
        public static Error UnknownError() {
            return Error.Failure("Portfolio.UnknownError", "UnknownError");
        }
    }
}