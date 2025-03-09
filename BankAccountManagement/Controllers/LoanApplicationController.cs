using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BankAccountManagement.Business.Contract;
using BankAccountManagement.Business.Repositories;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BankAccountManagement.Controllers
{
    public class LoanApplicationController : Controller
    {
        private readonly ILoanService _service;


        public LoanApplicationController(ILoanService service)
        {
            _service = service;
        }

        [HttpPost("loan")]
        public async Task<IActionResult> RequestNewLoanApplication([FromBody] LoanApplicationRequestDTO request)
        {
            var response = await _service.RequestForNewLoan(request);
            return Ok(response);
        }

        [HttpGet("application-status")]
        public async Task<IActionResult> GetApplicationStatus([FromQuery] int applicationId, [FromQuery] Guid userId )
        {
            var response = await _service.GetApplicationStatus(applicationId, userId);
            return Ok(response);
        }

        [HttpGet("getAll")]
        public async Task<IActionResult> GetAllApplications([FromQuery] Guid userId)
        {
            var response = await _service.GetAllApplications(userId);
            return Ok(response);
        }
    }
}

