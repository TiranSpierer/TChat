using Core.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces.DataServices;

public interface IMongoDbDataService<T> where T : IMongoDocument
{
    Task Add(T item);
    Task<T> GetById(string id);
    Task<List<T>> GetAll();
    Task Update(T item);
    Task Delete(T item);
}