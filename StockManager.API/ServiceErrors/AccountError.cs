namespace StockManager.API.ServiceErrors
{
    public static class AccountError
    {
        public static Error NotFound(Guid id) {
            return Error.NotFound("Account.NotFound", $"The account with id:{id} was not found");
        }
        public static Error EmailNotUnique(string? email) {
            return Error.Conflict("Account.EmailNotUnique", $"The account with email:{email} already exists. Please create account with another email");
        }
        public static Error EmptyFields() {
            return Error.Failure("Account.NullOrEmptyFields", "Request contained one or more empty or null fields.");
        }
        public static Error UnknownError() {
            return Error.Failure("Account.UnknownError", "Unknown error");
        }
    }
}