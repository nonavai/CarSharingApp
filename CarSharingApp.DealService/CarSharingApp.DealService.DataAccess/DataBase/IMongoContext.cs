using MongoDB.Driver;

namespace CarSharingApp.DealService.DataAccess.DataBase;

public interface IMongoContext
{
    public IMongoCollection<T> GetCollection<T>(string name);
}