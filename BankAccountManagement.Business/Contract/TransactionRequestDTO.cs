using System;
namespace BankAccountManagement.Business.Contract
{
	public class TransactionRequestDTO
	{
		public int SourceAccountNumber { get; set; }
		public int DestinationAccountNumber { get; set; }
		public decimal TransactionAmount { get; set; }
		public Guid UserId { get; set; }
	}
}

