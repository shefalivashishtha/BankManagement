using System;
namespace BankAccountManagement.Data.Helpers;

public class AccountNotFoundException : Exception
{
    public AccountNotFoundException(string message) : base(message) { }
}

public class NoAccountsFoundForTheUserException : Exception
{
    public NoAccountsFoundForTheUserException(string message) : base(message) { }
}

