using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.DataModels;
using Core.Interfaces.DataServices;
using System.Diagnostics;
using System;

namespace DataService.Repository;

public class MongoDbRepository : IMongoDbRepository
{
    private readonly IMongoDbContext _dbContext;

    public MongoDbRepository(IMongoDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<T>> GetAllAsync<T>() where T : IMongoDocument
    {
        var collection = GetCollection<T>();
        return await collection.Find(new BsonDocument()).ToListAsync();
    }

    public async Task<T> GetByIdAsync<T>(string id) where T : IMongoDocument
    {
        var collection = GetCollection<T>();
        var filter     = Builders<T>.Filter.Eq("_id", ObjectId.Parse(id));
        return await collection.Find(filter).FirstOrDefaultAsync();
    }

    public async Task AddAsync<T>(T item) where T : IMongoDocument
    {
        try
        {
            var collection = GetCollection<T>();
            await collection.InsertOneAsync(item);
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error adding item to collection: {ex.Message}");
            throw;
        }
    }

    public async Task<bool> UpdateAsync<T>(string id, T item) where T : IMongoDocument
    {
        var collection = GetCollection<T>();
        var filter     = Builders<T>.Filter.Eq("_id", ObjectId.Parse(id));
        var result     = await collection.ReplaceOneAsync(filter, item);

        return result.ModifiedCount > 0;
    }

    public async Task<bool> DeleteAsync<T>(string id) where T : IMongoDocument
    {
        var collection = GetCollection<T>();
        var filter     = Builders<T>.Filter.Eq("_id", ObjectId.Parse(id));
        var result     = await collection.DeleteOneAsync(filter);

        return result.DeletedCount > 0;
    }

    private IMongoCollection<T> GetCollection<T>()
    {
        var collectionName = typeof(T).Name.ToLower();
        Debug.WriteLine($"Getting collection for {collectionName}");
        return _dbContext.GetCollection<T>(collectionName);
    }
}