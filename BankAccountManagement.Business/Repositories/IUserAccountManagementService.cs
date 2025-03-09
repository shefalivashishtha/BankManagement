using System;
using BankAccountManagement.Data.Account;
using BankAccountManagement.Data.DataAccessor;
using BankAccountManagement.Business.Contract;

namespace BankAccountManagement.Business.Repositories
{
	public interface IUserAccountManagementService
	{
		Task<User> GetUserDetails(string userName);
		Task<UserAccount> GetUserAccountDetails(int accountNumber, Guid userId);
        Task<bool> PerformTransaction(TransactionRequestDTO transaction);
    }
    public class UserAccountManagementService : IUserAccountManagementService
    {
        private readonly IAccountAccessor _accountAccessor;
        private readonly ITransactionAccessor _transactionAccessor;
        private readonly IUserAccessor _userAccessor;

        public UserAccountManagementService(IAccountAccessor accountAccessor, IUserAccessor userAccessor,
            ITransactionAccessor transactionAccessor)
        {
            _accountAccessor = accountAccessor;
            _transactionAccessor = transactionAccessor;
            _userAccessor = userAccessor;
        }

        public async Task<UserAccount> GetUserAccountDetails(int accountNumber, Guid userId)
        {
            return await _accountAccessor.GetUserAccount(accountNumber, userId);
        }

        public async Task<User> GetUserDetails(string userName)
        {
            var userDetails = await _userAccessor.GetUserDetails(userName);
            if (userDetails != null)
            {
                userDetails.Accounts = await _accountAccessor.GetAllUserAccounts(userDetails.Id);
            }

            return userDetails;
        }

        public async Task<bool> PerformTransaction(TransactionRequestDTO request)
        {
            Transaction transaction = new Transaction
            {
                DestinationAccountNumer = request.DestinationAccountNumber,
                SourceAccountNumber = request.SourceAccountNumber,
                TransactionAmount = request.TransactionAmount,
                TransactionDate = DateTime.Now,
                UserId = request.UserId
            };
            return await _transactionAccessor.PerformTransaction(transaction);
        }
    }
}

