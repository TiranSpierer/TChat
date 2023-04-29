using DataService.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataService.Repository;

internal class Repository<T> : IRepository<T> where T : class
{
    public Task CreateAsync(T entity)
    {
        throw new System.NotImplementedException();
    }

    public Task DeleteAsync(object id)
    {
        throw new System.NotImplementedException();
    }

    public Task<IEnumerable<T>> GetAllAsync()
    {
        throw new System.NotImplementedException();
    }

    public Task<T?> GetByIdAsync(params object[] id)
    {
        throw new System.NotImplementedException();
    }

    public Task UpdateAsync(object id, T updatedEntity)
    {
        throw new System.NotImplementedException();
    }
}