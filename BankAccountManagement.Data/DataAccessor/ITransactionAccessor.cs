using System;
using System.IO;
using System.Text.Json;
using BankAccountManagement.Data.Account;
using BankAccountManagement.Data.Helpers;

namespace BankAccountManagement.Data.DataAccessor;

	public interface ITransactionAccessor
	{
		Task<bool> PerformTransaction(Transaction transaction);
	}

public class TransactionAccessor : ITransactionAccessor
{
    private readonly IUserAccessor _userAccessor;
    private readonly IAccountAccessor _accountAccessor;
    private readonly IFileWriter _writer;

    public TransactionAccessor(IUserAccessor userAccessor, IAccountAccessor accountAccessor, IFileWriter writer)
    {
        _userAccessor = userAccessor;
        _accountAccessor = accountAccessor;
        _writer = writer;
    }

    public async Task<bool> PerformTransaction(Transaction transaction)
    {
        try
        {
            string filePath = $"../MockDb/{transaction.UserId}Accounts.json";
           List<UserAccount> accounts = await _accountAccessor.GetAllUserAccounts(transaction.UserId);
            if (accounts != null && accounts.Count > 0)
            {
                if (transaction.TransactionType == TransactionType.Credit)
                {
                    return await TransferCredit(transaction, accounts, filePath);
                }
                else if (transaction.TransactionType == TransactionType.LoanCredit)
                {
                    return await TransferLoanCredit(transaction, accounts, filePath);
                }
                else throw new InvalidTransactionException("Invalid Transaction");
            }
            else throw new NoAccountsFoundForTheUserException("No active accounts found for the user");
               

        }
        catch (Exception ex)
        {
            throw;
        }
    }

    private async Task<bool> TransferLoanCredit(Transaction transaction, List<UserAccount> accounts, string filePath)
    {
        
        var destAccount = accounts?.Single(x => x.AccountNumber == transaction.DestinationAccountNumer);
        if (destAccount == null) throw new AccountNotFoundException("Destination Account Not Found");
        {
            accounts?.Remove(destAccount);
            destAccount.Balance += transaction.TransactionAmount;
        
            accounts.Add(destAccount);
            
                string writeJson = JsonSerializer.Serialize(accounts);
                await _writer.WriteToFile(filePath, writeJson);
            return true;
        }
    }

    private async Task<bool> TransferCredit(Transaction transaction, List<UserAccount> accounts, string filePath)
        {
            var sourceAccount = accounts?.Single(x => x.AccountNumber == transaction.SourceAccountNumber);
            var destAccount = accounts?.Single(x => x.AccountNumber == transaction.DestinationAccountNumer);
            if (sourceAccount == null) throw new AccountNotFoundException("Source Account Not Found");
            if (destAccount == null) throw new AccountNotFoundException("Destination Account Not Found");
            if (sourceAccount?.Balance < transaction.TransactionAmount) throw new InsufficientFundException("Insufficient fund in source account");
            else
            {
                accounts?.Remove(destAccount);
                accounts?.Remove(sourceAccount);
                destAccount.Balance += transaction.TransactionAmount;
                sourceAccount.Balance -= transaction.TransactionAmount;

                accounts.Add(destAccount);
                accounts.Add(sourceAccount);
                string writeJson = JsonSerializer.Serialize(accounts);
            await _writer.WriteToFile(filePath, writeJson);

                return true;
            }
        }
    }






