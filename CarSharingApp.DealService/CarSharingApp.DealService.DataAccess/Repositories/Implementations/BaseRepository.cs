using System.Linq.Expressions;
using CarSharingApp.DealService.DataAccess.DataBase;
using CarSharingApp.DealService.DataAccess.Entities;
using MongoDB.Driver;

namespace CarSharingApp.DealService.DataAccess.Repositories.Implementations;

public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
{
    protected readonly IMongoCollection<T> _collection;

    protected BaseRepository(IMongoContext context, string collectionName)
    {
        _collection = context.GetCollection<T>(collectionName);
    }
    
    public async Task<IEnumerable<T>> GetAllAsync(int currentPage, int pageSize, CancellationToken cancellationToken = default)
    {
        return await _collection.Find(_ => true)
            .Skip((currentPage - 1) * pageSize)
            .Limit(pageSize)
            .ToListAsync(cancellationToken: cancellationToken);
    }

    public async Task<T> GetByIdAsync(string id, CancellationToken cancellationToken = default)
    {
        return await _collection.Find(entity => entity.Id == id).FirstOrDefaultAsync(cancellationToken: cancellationToken);
    }

    public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> filter, CancellationToken cancellationToken = default)
    {
        return await _collection.Find(filter).ToListAsync(cancellationToken);
    }

    public async Task<T> CreateAsync(T entity, CancellationToken cancellationToken = default)
    {
        await _collection.InsertOneAsync(entity, cancellationToken: cancellationToken);
        
        return entity;
    }

    public async Task UpdateAsync(string id, T entity, CancellationToken cancellationToken = default)
    {
        await _collection.ReplaceOneAsync(Builders<T>.Filter.Eq("_id", id), entity, cancellationToken: cancellationToken);
    }

    public async Task DeleteAsync(string id, CancellationToken cancellationToken = default)
    {
        await _collection.DeleteOneAsync(Builders<T>.Filter.Eq("_id", id), cancellationToken: cancellationToken);
    }
}