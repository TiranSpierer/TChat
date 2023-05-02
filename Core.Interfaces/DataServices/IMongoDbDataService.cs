using Core.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces.DataServices;

public interface IMongoDbDataService
{
    Task<IEnumerable<User>> GetAllUsersAsync();
    Task                    AddUserAsync(User user);
}