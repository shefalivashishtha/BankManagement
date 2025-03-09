using BankAccountManagement.Business.Repositories;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BankAccountManagement.Controllers
{
    public class GenerateTestDataController : Controller
    {
        private readonly ITestGeneratorService _service;

        public GenerateTestDataController(ITestGeneratorService service)
        {
            _service = service;
        }

        [HttpGet("test")]
        public async Task<IActionResult> GenerateTest()
        {
            await _service.AddUsersWithAccount();
            return Ok();
        }
        
    }
}

