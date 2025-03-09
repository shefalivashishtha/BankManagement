using System;
namespace BankAccountManagement.Data
{
	public static class Constants
	{

        public enum AccountType
        {
            Current,
            Savings,
            Loan
        }

        public enum ApplicationStatus
        {
            Approved,
            Rejected
        }
    }
}

