using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StockManager.API.ServiceErrors;
using StockManager.Contracts.Error;

namespace StockManager.API.Mapper
{
    public class ErrorMapper
    {
        public static ErrorResponse ToResponse(Error error) {
            return new ErrorResponse
            (
                ErrorCode: error.Code,
                Description: error.Description
            );
        }
    }
}