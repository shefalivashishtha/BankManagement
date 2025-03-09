using System;
using BankAccountManagement.Data.Account;
using static BankAccountManagement.Data.Constants;

namespace BankAccountManagement.Data.LoanApplication
{
	public class Application
	{
		public int? ApplicationId { get; set; }
		public int LoanDuration { get; set; }
		public decimal LoanAmount { get; set; }
		public User User { get; set; }
		public ApplicationStatus ApplicationStatus { get; set; }
        public int LoanAccountNumber { get; set; }

        public decimal InterestRate { get {return GetApplicableInterestRate(); } }

        //ideally this data would be fetched from database but have added hardcoded implementation based on the provided data
        // LoanInterest.cs indicates the db schema for this table
        private decimal GetApplicableInterestRate()
        {
			decimal interest = -1;
            switch (this.LoanDuration)
			{
				case 1:
					{
						if (this.User.CreditRating >= 20 && this.User.CreditRating <= 50) interest = 20;
                        else if (this.User.CreditRating >= 50 && this.User.CreditRating <= 100) interest = 12;
                        break;
					}
                case 3:
                    {
                        if (this.User.CreditRating >= 20 && this.User.CreditRating <= 50) interest = 15;
                        else if (this.User.CreditRating >= 50 && this.User.CreditRating <= 100) interest = 8;
                        break;
                    }
                case 5:
                    {
                        if (this.User.CreditRating >= 20 && this.User.CreditRating <= 50) interest = 10;
                        else if (this.User.CreditRating >= 50 && this.User.CreditRating <= 100) interest = 5;
                        break;
                    }
            }
			return interest;
		}

    }


	
    
}

