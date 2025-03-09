using System;
namespace BankAccountManagement.Business.Contract
{
	public class LoanApplicationRequestDTO
	{
		public int Duration { get; set; }
		public decimal LoanAmount { get; set; }
		public Guid UserId { get; set; }
	}
}

