using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StockManager.Contracts.Account;

namespace StockManager.API.Controllers
{
    [Route("/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        [HttpPost("/create-account")]
        public IActionResult CreateAccount(CreateAccountRequest req) {
            
            return Ok(req);
        }
        [HttpGet("/get-account/{id:guid}")]
        public IActionResult GetAccount(Guid id) {
            return Ok(id);
        }

        [HttpPut("/update-account/{id:guid}")]
        public IActionResult UpdateAccount(Guid id, UpdateAccountRequest req) {
            return Ok(req); 
        }

        [HttpDelete("/delete-account/{id:Guid}")]
        public IActionResult DeleteAccount(Guid id) {
            return Ok(id);
        }
    }
}