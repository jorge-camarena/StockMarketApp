using Microsoft.AspNetCore.Mvc;
using StockManager.Contracts.Account;
using StockManager.API.Models;
using StockManager.API.MicroServices.AccountService;
using Microsoft.AspNetCore.Identity;
using System.Security.Principal;

// TODO:
//      1.Figure out if I should send password in AccountResponse
//      2.
    


namespace StockManager.API.Controllers
{
    [ApiController]
    [Route("account")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService = new AccountService( new());

        [HttpPost("create-account")]
        public IActionResult CreateAccount(CreateAccountRequest req) {
            try 
            {
                Account account = _accountService.CreateAccount(req);
                AccountResponse response = new AccountResponse(
                    Id: account.Id,
                    Name: account.Name,
                    Email: account.Email,
                    AccountType: account.AccountType,
                    CreatedAtDateTime: account.CreatedAtDateTime,
                    LastUpdatedDateTime: account.LastUpdatedDateTime
                );
                return Ok(response);
            } catch(Microsoft.EntityFrameworkCore.DbUpdateException) 
            {
                return BadRequest("Unable to save account");
            }

        }
        [HttpGet("get-account/{id:guid}")]
        public IActionResult GetAccount(Guid id) {
            Account account = _accountService.GetAccount(id);
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
            Account account = _accountService.UpdateAccount(req);
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
            Account account = _accountService.DeleteAccount(id);

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