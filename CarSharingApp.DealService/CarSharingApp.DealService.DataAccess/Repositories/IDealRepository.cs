using CarSharingApp.DealService.DataAccess.Entities;

namespace CarSharingApp.DealService.DataAccess.Repositories;

public interface IDealRepository : IBaseRepository<Deal>
{
    Task<IEnumerable<Deal>> GetByCarIdAsync(string carId, int currentPage,
        int pageSize, CancellationToken cancellationToken);
    Task<IEnumerable<Deal>> GetByUserIdAsync(string userId, int currentPage,
        int pageSize, CancellationToken cancellationToken);
}