using Core.Interfaces.DataServices;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataService.Services;

public class MongoDbDataService<T> : IMongoDbDataService<T> where T : IMongoDocument
{
    private readonly IMongoDbRepository<T> _repository;

    public MongoDbDataService(IMongoDbRepository<T> repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<T>> GetAll()
    {
        return await _repository.GetAll();
    }

    public async Task<T> GetById(string id)
    {
        return await _repository.GetById(id);
    }

    public async Task Add(T item)
    {
        await _repository.Add(item);
    }

    public async Task<bool> Update(string id, T item)
    {
        return await _repository.Update(id, item);
    }

    public async Task<bool> Delete(string id)
    {
        return await _repository.Delete(id);
    }
}