using Core.DataModels;
using Core.Interfaces.DataServices;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataService.Services;

public class MongoDbDataService
{
    private readonly IMongoDbContext _dbContext;

    public MongoDbDataService(IMongoDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<T>> GetAll<T>() where T : IMongoDocument
    {
        var collection = _dbContext.GetCollection<T>(typeof(T).Name.ToLower());
        var result = await collection.FindAsync(new BsonDocument());
        return await result.ToListAsync();
    }

    public async void X()
    {
        await GetAll<User>();
    }
}