using System;
namespace BankAccountManagement.Data.Account
{
	public class Transaction
	{
		public int SourceAccountNumber { get; set; }
		public int DestinationAccountNumer { get; set; }
		public decimal TransactionAmount { get; set; }
		public DateTime TransactionDate { get; set; }
		public TransactionType TransactionType { get; set; } = TransactionType.Credit;
		public Guid UserId { get; set; }
	}

	public enum TransactionType
	{
		Credit,
		LoanCredit
	}
}

