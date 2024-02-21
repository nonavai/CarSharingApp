using CarSharingApp.DealService.DataAccess.DataBase;
using CarSharingApp.DealService.DataAccess.Entities;
using MongoDB.Driver;

namespace CarSharingApp.DealService.DataAccess.Repositories.Implementations;

public class DealRepository : BaseRepository<Deal>, IDealRepository
{
    public DealRepository(MongoContext context) : base(context, "Deals")
    {
    }

    public async Task<IEnumerable<Deal>> GetByCarIdAsync(string carId, int currentPage, int pageSize, CancellationToken cancellationToken = default)
    {
        return await _collection.Find(deal => deal.CarId == carId)
            .SortBy(deal => deal.Finished)
            .Skip((currentPage - 1) * pageSize)
            .Limit(pageSize)
            .ToListAsync(cancellationToken: cancellationToken);
    }

    public async Task<IEnumerable<Deal>> GetByUserIdAsync(string userId, int currentPage, int pageSize, CancellationToken cancellationToken = default)
    {
        return await _collection.Find(deal => deal.UserId == userId)
            .SortBy(deal => deal.Finished)
            .Skip((currentPage - 1) * pageSize)
            .Limit(pageSize)
            .ToListAsync(cancellationToken: cancellationToken);
    }
}