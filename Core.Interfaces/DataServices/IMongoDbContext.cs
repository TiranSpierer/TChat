// Created by Tiran Spierer
// Created at 01/05/2023
// Class purpose:

using MongoDB.Driver;

namespace Core.Interfaces.DataServices;

public interface IMongoDbContext
{
    IMongoCollection<T> GetCollection<T>(string name);
}