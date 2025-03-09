using System;
using System.Text.Json;
using BankAccountManagement.Data.Account;
using BankAccountManagement.Data.Helpers;

namespace BankAccountManagement.Data.DataAccessor;

public interface IUserAccessor
{
    Task<User> GetUserDetails(string userName);
    Task<User> GetUserDetails(Guid userId);
}

public class UserAccessor : IUserAccessor
{
    private readonly IFileReader _reader;

    public UserAccessor(IFileReader reader)
    {
        _reader = reader;
    }

    public async Task<User> GetUserDetails(Guid userId)
    {
        var users = await GetAllUsers();
        var user = users.Single(x => x.Id == userId);
        if (user != null) return user;
        else throw new UserNotFoundException("No User found");
    }

    public async Task<User> GetUserDetails(string userName)
    {
        try
        {
            var allUsers = await GetAllUsers();
            var user = allUsers.Single(x => x.Name == userName);
            if (user != null) return user;
            else throw new UserNotFoundException("No User found");

        }
        catch
        {
            throw;
        }

        throw new NoUsersFoundException("No users found");
    }

    private async Task<IEnumerable<User>> GetAllUsers()
    {
        try
        {
            string filePath = $"../MockDb/Users.json";
            
            var usersJson = await _reader.ReadSerializedData(filePath);
            if (!string.IsNullOrEmpty(usersJson))
            {
                List<User> allUsers = JsonSerializer.Deserialize<List<User>>(usersJson);
                return allUsers;
            }
            else throw new NoUsersFoundException("No Users Found. Try /api/generateTest endpoint to generate some sample data");
        }
        catch
        {
            throw;
        }
    }
}



