using Core.DataModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Interfaces.DataServices;

public interface IMongoDbRepository
{
    Task<IEnumerable<T>> GetAllAsync<T>() where T : IMongoDocument;

    Task<T> GetByIdAsync<T>(string id) where T : IMongoDocument;

    Task AddAsync<T>(T item) where T : IMongoDocument;

    Task<bool> UpdateAsync<T>(string id, T item) where T : IMongoDocument;

    Task<bool> DeleteAsync<T>(string id) where T : IMongoDocument;
}