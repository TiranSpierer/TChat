using Core.DataModels;
using Core.Interfaces.DataServices;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace DataService.Services;

public class MongoDbDataService : IMongoDbDataService
{
    private readonly IMongoDbRepository _repository;

    public MongoDbDataService(IMongoDbRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<User>> GetAllUsersAsync()
    {
        return await _repository.GetAllAsync<User>();
    }

    public async Task AddUserAsync(User user)
    {
        try
        {
            await _repository.AddAsync(user);
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error adding user: {ex.Message}");
            throw;
        }
    }
}