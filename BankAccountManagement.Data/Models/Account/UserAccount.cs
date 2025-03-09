using System;
using static BankAccountManagement.Data.Constants;

namespace BankAccountManagement.Data.Account
{
	public class UserAccount
	{
		// foreign key reference
		public User User { get; set; }
		public int AccountNumber { get; set; }
		public decimal Balance { get; set; }
		public AccountType AccountType { get; set; }
		public DateTime CreatedDate { get; set; }
		public DateTime ModifiedDate { get; set; }
	}

	public class LoanAccount : UserAccount
	{
		public decimal OutstandingBalance { get; set; }
	}

}

