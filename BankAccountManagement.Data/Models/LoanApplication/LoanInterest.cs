using System;
namespace BankAccountManagement.Data.LoanApplication
{
	public class LoanInterest
	{
		public decimal InterestRate { get; set; }
		public int Duration { get; set; }
		public int MinCreditRating { get; set; }
        public int MaxCreditRating { get; set; }
    }

}

