using System;
namespace BankAccountManagement.Data.Helpers;

public class UserNotFoundException : Exception
{
    public UserNotFoundException(string message) : base(message) { }
}

public class NoUsersFoundException : Exception
{
    public NoUsersFoundException(string message) : base(message) { }
}

