using System;
namespace BankAccountManagement.Data.Helpers;

public class LoanApplicationNotFoundException : Exception
{
    public LoanApplicationNotFoundException(string message)
        : base(message) { }
    public LoanApplicationNotFoundException(string message, Exception exception)
        : base(message, exception) { }
}
public class NoLoanApplicationFoundForUserException : Exception
{
    public NoLoanApplicationFoundForUserException(string message)
        : base(message) { }
    public NoLoanApplicationFoundForUserException(string message, Exception exception)
        : base(message, exception) { }
}



