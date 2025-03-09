using static BankAccountManagement.Data.Constants;
using System.Text.Json;
using Application = BankAccountManagement.Data.LoanApplication.Application;
using BankAccountManagement.Data.Helpers;

namespace BankAccountManagement.Data.DataAccessor;

	public interface ILoanApplicationAccessor
	{
		Task<Application> CreateLoanApplication(Application application);
		Task<ApplicationStatus> GetApplicationStatus(int applicationId, Guid userId);
        Task<List<Application>> GetAllApplications(Guid userId);
	}

public class LoanApplicationAccessor : ILoanApplicationAccessor
{
    private readonly IFileReader _reader;
    private readonly IFileWriter _writer;

    public LoanApplicationAccessor(IFileReader reader, IFileWriter writer)
    {
        _reader = reader;
        _writer = writer;
    }

    public async Task<Application> CreateLoanApplication(Application application)
    {
        string filePath = $"../MockDb/{application.User.Id}Applications.json";
        var existingApplications = await GetAllApplications(application.User.Id);
        if (existingApplications != null && existingApplications.Count > 0)
            application.ApplicationId = existingApplications.Max(x => x.ApplicationId) + 1;
        else application.ApplicationId = 1;
        existingApplications.Add(application);
        string writeJson = JsonSerializer.Serialize(existingApplications);
        await _writer.WriteToFile(filePath, writeJson);
      
        return application;
    }

    public async Task<List<Application>> GetAllApplications(Guid userId)
    {
        string filePath = $"../MockDb/{userId}Applications.json";
        var applicationsJson = await _reader.ReadSerializedData(filePath);
        if (!string.IsNullOrEmpty(applicationsJson))
        {
            return JsonSerializer.Deserialize<List<Application>>(applicationsJson);
        }
        else return new List<Application>();
    }

    public async Task<ApplicationStatus> GetApplicationStatus(int applicationId, Guid userId)
    {
        var applications = await GetAllApplications(userId);
        if (applications != null && applications.Count > 0)
        {
            var selectedApplication = applications.Single(x => x.ApplicationId == applicationId);
            if (selectedApplication != null) return selectedApplication.ApplicationStatus;
            else throw new LoanApplicationNotFoundException("Invalid Application");
        }
        else throw new NoLoanApplicationFoundForUserException("No active loan application");
    }
}

