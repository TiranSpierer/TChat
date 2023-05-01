using Core.DataModels;
using System.Collections.Generic;

namespace Core.Interfaces.DataServices;
public interface IUserDataService
{
    bool AuthenticateUser(string userName, string password);
    void AddUser(User user);
    User GetUser(string userName);
    ICollection<User> GetUsers();
}