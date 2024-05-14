using Microsoft.AspNetCore.Mvc;
using StockManager.Contracts.Account;
using StockManager.Contracts.Error;
using StockManager.API.Models;
using StockManager.API.MicroServices.AccountService;
using StockManager.API.ServiceErrors;

namespace StockManager.API.Controllers
{
    [ApiController]
    [Route("account")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService = new AccountService( new());

        [HttpPost("create-account")]
        public IActionResult CreateAccount(CreateAccountRequest req) {
            var result = _accountService.CreateAccount(req);
            if (result.Error is not null) {
                Error error = result.Error;
                ErrorResponse errorResponse = new ErrorResponse(
                    ErrorCode: error.Code,
                    Description: error.Description
                );
                if (error.Code == "Account.EmailNotUnique") {
                    return Conflict(errorResponse);
                } else {
                    return BadRequest(errorResponse);
                }
            }
            Account? account = result.Value;
            AccountResponse response = new AccountResponse(
                Id: account.Id,
                Name: account.Name,
                Email: account.Email,
                AccountType: account.AccountType,
                CreatedAtDateTime: account.CreatedAtDateTime,
                LastUpdatedDateTime: account.LastUpdatedDateTime
            );
            return Ok(response);

        }
        [HttpGet("get-account/{id:guid}")]
        public IActionResult GetAccount(Guid id) {
            var result = _accountService.GetAccount(id);
            if (result.Error is not null) {
                Error? error = result.Error;
                ErrorResponse errorResponse = new ErrorResponse(
                    ErrorCode: error.Code,
                    Description: error.Description
                );
                return NotFound(errorResponse);
            }
            Account? account = result.Value;
                AccountResponse response = new AccountResponse(
                Id: account.Id,
                Name: account.Name,
                Email: account.Email,
                AccountType: account.AccountType,
                CreatedAtDateTime: account.CreatedAtDateTime,
                LastUpdatedDateTime: account.LastUpdatedDateTime
            );
            return Ok(response);
        }

        [HttpPut("update-account")]
        public IActionResult UpdateAccount(UpdateAccountRequest req) {
            var result = _accountService.UpdateAccount(req);
            if (result.Error is not null) {
                Error error = result.Error;
                ErrorResponse errorResponse = new ErrorResponse(
                    ErrorCode: error.Code,
                    Description: error.Description
                );
                if (error.Code == "Account.NotFound"){
                    return NotFound(errorResponse);
                } else {
                    return BadRequest(errorResponse);

                }  
            }
            Account? account = result.Value;
            AccountResponse response = new AccountResponse(
                Id: account.Id,
                Name: account.Name,
                Email: account.Email,
                AccountType: account.AccountType,
                CreatedAtDateTime: account.CreatedAtDateTime,
                LastUpdatedDateTime: DateTime.UtcNow
            );
            return Ok(response); 
        }

        [HttpDelete("delete-account/{id:Guid}")]
        public IActionResult DeleteAccount(Guid id) {
            var result = _accountService.DeleteAccount(id);
            if (result.Error is not null) {
                Error error = result.Error;
                ErrorResponse errorResponse = new ErrorResponse(
                    ErrorCode: error.Code,
                    Description: error.Description
                );
                if (error.Code == "Account.NotFound") {
                    return NotFound(errorResponse);
                } else {
                    return BadRequest(errorResponse);
                }
            }
            Account? account = result.Value;
            AccountResponse response = new AccountResponse(
            Id: account.Id,
            Name: account.Name,
            Email: account.Email,
            AccountType: account.AccountType,
            CreatedAtDateTime: account.CreatedAtDateTime,
            LastUpdatedDateTime: account.LastUpdatedDateTime
            );
            return Ok(response); 
        }
    }
}