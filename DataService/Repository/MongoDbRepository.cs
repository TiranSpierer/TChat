using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Interfaces.DataServices;

namespace DataService.Repository;

public class MongoDbRepository<T> : IMongoDbRepository<T>
{
    private readonly IMongoCollection<T> _collection;

    public MongoDbRepository(IMongoDbContext dbContext)
    {
        _collection = dbContext.GetCollection<T>(typeof(T).Name.ToLower() + "s");
    }

    public async Task<IEnumerable<T>> GetAll()
    {
        return await _collection.Find(new BsonDocument()).ToListAsync();
    }

    public async Task<T> GetById(string id)
    {
        var filter = Builders<T>.Filter.Eq("_id", ObjectId.Parse(id));
        return await _collection.Find(filter).FirstOrDefaultAsync();
    }

    public async Task Add(T item)
    {
        await _collection.InsertOneAsync(item);
    }

    public async Task<bool> Update(string id, T item)
    {
        var filter = Builders<T>.Filter.Eq("_id", ObjectId.Parse(id));
        var result = await _collection.ReplaceOneAsync(filter, item);

        return result.ModifiedCount > 0;
    }

    public async Task<bool> Delete(string id)
    {
        var filter = Builders<T>.Filter.Eq("_id", ObjectId.Parse(id));
        var result = await _collection.DeleteOneAsync(filter);

        return result.DeletedCount > 0;
    }
}