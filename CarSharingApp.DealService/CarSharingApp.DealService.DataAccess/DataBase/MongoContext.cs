using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace CarSharingApp.DealService.DataAccess.DataBase;

public class MongoContext : IMongoContext
{
    private readonly IMongoDatabase _database;
    
    public MongoContext(IConfiguration config)
    {
        
        var client = new MongoClient(config.GetConnectionString("Database"));
        _database = client.GetDatabase(config.GetConnectionString("DatabaseName"));
    }

    public IMongoCollection<T> GetCollection<T>(string name)
    {
        return _database.GetCollection<T>(name);
    }
}