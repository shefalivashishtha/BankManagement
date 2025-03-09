using BankAccountManagement.Business.Repositories;
using BankAccountManagement.Business.Contract;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BankAccountManagement.Controllers
{
    public class UserAccountManagementController : Controller
    {
        private readonly IUserAccountManagementService _managementService;

        public UserAccountManagementController(IUserAccountManagementService managementService)
        {
            _managementService = managementService;
        }

        [HttpGet("user-profile")]
        public async Task<IActionResult> GetUserDetailsWithAllAccounts([FromQuery] string userName)
        {
            var response = await _managementService.GetUserDetails(userName);
            return Ok(response);
        }

        [HttpGet("account")]
        public async Task<IActionResult> GetUserAccountDetails([FromQuery] Guid userId, [FromQuery] int accountNumber)
        {
            var response = await _managementService.GetUserAccountDetails(accountNumber, userId);
            return Ok(response);
        }

        [HttpPost("transfer")]
        public async Task<IActionResult> TransferMoney([FromBody]TransactionRequestDTO request)
        {
            var response = await _managementService.PerformTransaction(request);
            return Ok(response);
        }
    }
}

