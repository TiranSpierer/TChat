using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Interfaces.DataServices;

public interface IMongoDbRepository<T>
{
    Task<IEnumerable<T>> GetAll();
    Task<T>              GetById(string id);
    Task                 Add(T          item);
    Task<bool>           Update(string  id, T item);
    Task<bool>           Delete(string  id);
}