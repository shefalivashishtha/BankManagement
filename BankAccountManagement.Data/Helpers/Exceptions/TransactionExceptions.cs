using System;
namespace BankAccountManagement.Data.Helpers
{
    public class InsufficientFundException : Exception
    {
        public InsufficientFundException(string message) : base(message) { }
    }

    public class InvalidTransactionException : Exception
    {
        public InvalidTransactionException(string message) : base(message) { }
    }
}

