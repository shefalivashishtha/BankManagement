using System;
using BankAccountManagement.Business.Contract;
using BankAccountManagement.Data.Account;
using BankAccountManagement.Data.DataAccessor;
using BankAccountManagement.Data.LoanApplication;
using static BankAccountManagement.Data.Constants;

namespace BankAccountManagement.Business.Repositories
{
	public interface ILoanService
	{
        Task<Application> RequestForNewLoan(LoanApplicationRequestDTO request);
        Task<ApplicationStatus> GetApplicationStatus(int applicationNumber, Guid userId);
        Task<IEnumerable<Application>> GetAllApplications(Guid userId);
    }

    public class LoanService : ILoanService
    {
        private readonly ILoanApplicationAccessor _applicationAccessor;
        private readonly IUserAccessor _userAccessor;
        private readonly IAccountAccessor _accountAccessor;
        private readonly ITransactionAccessor _transactionAccessor;

        public LoanService(ILoanApplicationAccessor applicationAccessor, IUserAccessor userAccessor,
            IAccountAccessor accountAccessor, ITransactionAccessor transactionAccessor)
        {
            _applicationAccessor = applicationAccessor;
            _userAccessor = userAccessor;
            _accountAccessor = accountAccessor;
            _transactionAccessor = transactionAccessor;

        }

        public async Task<IEnumerable<Application>> GetAllApplications(Guid userId)
        {
            return await _applicationAccessor.GetAllApplications(userId);
        }

        public async Task<ApplicationStatus> GetApplicationStatus(int applicationNumber, Guid userId)
        {
            return await _applicationAccessor.GetApplicationStatus(applicationNumber,userId);
        }

        public async Task<Application> RequestForNewLoan(LoanApplicationRequestDTO request)
        {
            
            var user = await _userAccessor.GetUserDetails(request.UserId);
            bool isEligible = IsEligibleForLoan(user.CreditRating, request.LoanAmount, request.Duration);
            LoanAccount account = new LoanAccount();
            if (isEligible)
            {
                //create loan account

                account.AccountType = AccountType.Loan;
                account.Balance = request.LoanAmount;
                account.CreatedDate = DateTime.Now;
                account.OutstandingBalance = -1 * request.LoanAmount;
                account.User = user;

                account = await _accountAccessor.CreateLoanAccount(account);

                Transaction transact = new Transaction
                {
                    DestinationAccountNumer = account.AccountNumber,
                    TransactionAmount = request.LoanAmount,
                    TransactionDate = DateTime.Now,
                    UserId = request.UserId,
                    TransactionType = TransactionType.LoanCredit,
                };

                 await _transactionAccessor.PerformTransaction(transact);

            }

                Application loanApplication = new Application
                {
                    ApplicationStatus = isEligible ? ApplicationStatus.Approved : ApplicationStatus.Rejected,
                    LoanAmount = request.LoanAmount,
                    LoanDuration = request.Duration,
                    User = user,
                    LoanAccountNumber = isEligible ?  account.AccountNumber : default
                };
            
            return await _applicationAccessor.CreateLoanApplication(loanApplication);

        }

        private bool IsEligibleForLoan(int creditRating, decimal loanAmount, int loanDuration) => creditRating >= 20 && loanAmount < 10000 && (loanDuration == 1 || loanDuration == 3 || loanDuration == 5);
    }
}

