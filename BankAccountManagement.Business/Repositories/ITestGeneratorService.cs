using System;
using BankAccountManagement.Data.DataAccessor;

namespace BankAccountManagement.Business.Repositories
{
	public interface ITestGeneratorService
	{
		Task AddUsersWithAccount();
    }
	public class TestGeneratorService :ITestGeneratorService
	{ 
        private readonly ITestGeneratorAccessor _tester;

        public TestGeneratorService(ITestGeneratorAccessor tester)
		{
			_tester = tester;
		}
        public async Task AddUsersWithAccount()
		{
			await _tester.AddUsersWithAccount();
		}
    }
}

