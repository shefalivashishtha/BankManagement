using System;
using System.Text.Json;
using BankAccountManagement.Data.Account;
using BankAccountManagement.Data.Helpers;

namespace BankAccountManagement.Data.DataAccessor;

	public interface IAccountAccessor
	{
		Task<UserAccount> GetUserAccount(int accountNumber, Guid userId);
        Task<List<UserAccount>> GetAllUserAccounts( Guid userId);
    Task<LoanAccount> CreateLoanAccount(LoanAccount account);
}

public class AccountAccessor : IAccountAccessor
{
    private readonly IFileWriter _writer;
    private readonly IFileReader _reader;

    public AccountAccessor(IFileWriter writer, IFileReader reader)
    {
        _writer = writer;
        _reader = reader;
    }

    public async Task<List<UserAccount>> GetAllUserAccounts(Guid userId)
    {
        string filePath = $"../MockDb/{userId}Accounts.json";
        var accountsJson = await _reader.ReadSerializedData(filePath);
        if (!string.IsNullOrEmpty(accountsJson))
        {
            List<UserAccount> allUserAccounts = JsonSerializer.Deserialize<List<UserAccount>>(accountsJson);
            return allUserAccounts;
        }
        else return new List<UserAccount>();
    }

    public async Task<UserAccount> GetUserAccount(int accountNumber, Guid userId)
    {
        var allUserAccounts = await GetAllUserAccounts(userId);
        if (allUserAccounts != null && allUserAccounts.Count() > 0)
        {
            var account = allUserAccounts.Single(x => x.AccountNumber == accountNumber);
            if (account != null) return account;
            else throw new AccountNotFoundException("No User found");
        }
        else throw new NoAccountsFoundForTheUserException("No accounts found");

    }


    public async Task<LoanAccount> CreateLoanAccount(LoanAccount account)
    {
        var existingAccounts = await GetAllUserAccounts(account.User.Id);
        string filePath = $"../MockDb/{account.User.Id}Accounts.json";
        if (existingAccounts != null && existingAccounts.Count > 0)
            account.AccountNumber = existingAccounts.Max(x => x.AccountNumber) + 1;
        else account.AccountNumber = 1;
        existingAccounts.Add(account);
        string writeJson = JsonSerializer.Serialize(existingAccounts);
        await _writer.WriteToFile(filePath, writeJson);
        return account;

    }
}

