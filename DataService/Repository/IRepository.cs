using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataService.Repository;
internal interface IRepository<T> where T : class
    { 
    Task CreateAsync(T entity);

    Task<T?> GetByIdAsync(params object[] id);

    Task<IEnumerable<T>> GetAllAsync();

    Task UpdateAsync(object id, T updatedEntity);

    Task DeleteAsync(object id);
}
