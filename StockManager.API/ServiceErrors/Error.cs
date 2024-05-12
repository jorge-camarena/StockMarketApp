using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockManager.API.ServiceErrors
{
    public class Error
    {
        public string? Code { get; }
        public string? Description { get; }
        public ErrorType ErrorType { get; }

        private Error(string code, string description, ErrorType errorType)
        {
            Code = code;
            Description = description;
            ErrorType = errorType;
        }
        public static Error NotFound(string code, string description) {
            return new Error(code, description, ErrorType.NotFound);
        }
        public static Error Conflict(string code, string description) {
            return new Error(code, description, ErrorType.Conflict);
        }
        public static Error Validation(string code, string description) {
            return new Error(code, description, ErrorType.Validation);
        }
        public static Error Failure(string code, string description) {
            return new Error(code, description, ErrorType.Failure);
        }
    }
}
public enum ErrorType
{
    Failure,
    Validation,
    NotFound,
    Conflict
}