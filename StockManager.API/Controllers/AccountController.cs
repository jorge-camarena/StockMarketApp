using Microsoft.AspNetCore.Mvc;
using StockManager.Contracts.Account;
using StockManager.Contracts.Error;
using StockManager.API.Models;
using StockManager.API.MicroServices.AccountService;
using StockManager.API.ServiceErrors;
using StockManager.API.Mapper;

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
            if (result._success) {
                Account? account = result.Value;
                AccountResponse response = AccountMapper.ToResponse(account);
                return Ok(response);
            } else {
                Error? error = result.Error;
                ErrorResponse errorResponse = ErrorMapper.ToResponse(error);
                if (error.Code == "Account.EmailNotUnique") {
                    return Conflict(errorResponse);
                } else {
                    return BadRequest(errorResponse);
                }
            }
        }
        [HttpGet("get-account/{id:guid}")]
        public IActionResult GetAccount(Guid id) {
            var result = _accountService.GetAccount(id);
            if (result._success) {
                Account? account = result.Value;
                AccountResponse response = AccountMapper.ToResponse(account);
                return Ok(response);
            } else {
                Error? error = result.Error;
                ErrorResponse errorResponse = ErrorMapper.ToResponse(error);
                return NotFound(errorResponse);
            }
        }

        [HttpPut("update-account")]
        public IActionResult UpdateAccount(UpdateAccountRequest req) {
            var result = _accountService.UpdateAccount(req);
            if (result._success) {
                Account? account = result.Value;
                AccountResponse response = AccountMapper.ToResponse(account);
                return Ok(response);
            } else {
                Error error = result.Error;
                ErrorResponse errorResponse = ErrorMapper.ToResponse(error);
                if (error.Code == "Account.NotFound"){
                    return NotFound(errorResponse);
                } else {
                    return BadRequest(errorResponse);
                }
            }
        }

        [HttpDelete("delete-account/{id:Guid}")]
        public IActionResult DeleteAccount(Guid id) {
            var result = _accountService.DeleteAccount(id);
            if (result._success) {
                Account? account = result.Value;
                AccountResponse response = AccountMapper.ToResponse(account);
                return Ok(response);
            } else {
                Error error = result.Error;
                ErrorResponse errorResponse = ErrorMapper.ToResponse(error);
                if (error.Code == "Account.NotFound") {
                    return NotFound(errorResponse);
                } else {
                    return BadRequest(errorResponse);
                }
            }
        }
    }
}