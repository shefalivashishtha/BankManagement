using System;
using System.IO;
using System.Reflection.Metadata;
using System.Security.Principal;
using System.Text.Json;
using BankAccountManagement.Data.Account;
using BankAccountManagement.Data.Helpers;

namespace BankAccountManagement.Data.DataAccessor;

	public interface ITestGeneratorAccessor
	{
		Task AddUsersWithAccount();
	}

	public class TestGeneratorAccessor : ITestGeneratorAccessor
	{
    private readonly IFileWriter _writer;

    public TestGeneratorAccessor(IFileWriter writer)
    {
        _writer = writer;
    }

    public async Task AddUsersWithAccount()
    {

        string userFilePath = $"../MockDb/Users.json";

        User Bob = new User
        {
            Name = "Bob",
            CreditRating = 15,
            CreatedDate = DateTime.Now,
            Id = Guid.NewGuid(),
            IsActive = true,
            
        };

        User Jim = new User
        {
            Name = "Jim",
            CreditRating = 45,
            CreatedDate = DateTime.Now,
            Id = Guid.NewGuid(),
            IsActive = true,
            
        };

        User Anne = new User
        {
            Name = "Anne",
            CreditRating = 80,
            CreatedDate = DateTime.Now,
            Id = Guid.NewGuid(),
            IsActive = true,
        };

        List<User> users = new List<User>() { Bob, Jim, Anne };

        string writeJson = JsonSerializer.Serialize(users);
        await _writer.WriteToFile(userFilePath, writeJson);

        var BobAccounts = new List<UserAccount>() {
            new UserAccount() { AccountType = Constants.AccountType.Current, Balance = 1500, CreatedDate = DateTime.Now, AccountNumber = 1 } };
        await CreateUserAccount(BobAccounts, Bob.Id);

        var JimAccounts = new List<UserAccount>() { new UserAccount() { AccountNumber = 1, AccountType = Constants.AccountType.Savings, Balance = 1200, CreatedDate = DateTime.Now } };
         await CreateUserAccount(JimAccounts, Jim.Id);
        var AnneAccounts = new List<UserAccount>() {
        new UserAccount() {AccountNumber = 1, AccountType = Constants.AccountType.Savings, Balance = 12000, CreatedDate = DateTime.Now },
        new UserAccount() {AccountNumber = 2, AccountType = Constants.AccountType.Current, Balance = 10000, CreatedDate = DateTime.Now } };
        await CreateUserAccount(AnneAccounts, Anne.Id);

    }

    private async Task CreateUserAccount(List<UserAccount> accounts, Guid userId)
    {
        string userAccountFilePath = $"../MockDb/{userId}Accounts.json";
        string writeJson = JsonSerializer.Serialize(accounts);
        await _writer.WriteToFile(userAccountFilePath, writeJson);
    }


}

