using System;
namespace BankAccountManagement.Data.Account
{
	public class User
	{
		// Primary key for User Table
        public Guid Id { get; set; }
        public string Name { get; set; }
		public int CreditRating { get; set; }
		public bool IsActive { get; set; }
		public DateTime CreatedDate { get; set; }
		public DateTime ModifiedDate { get; set; }
		// this will be fetched based on the foreign key reference of UserId in UserAccount Table
		public IEnumerable<UserAccount> Accounts { get; set; }
	}
}

