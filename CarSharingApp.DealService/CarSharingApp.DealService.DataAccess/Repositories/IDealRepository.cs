using CarSharingApp.DealService.DataAccess.Entities;

namespace CarSharingApp.DealService.DataAccess.Repositories;

public interface IDealRepository : IBaseRepository<Deal>
{
    Task<IEnumerable<Deal>> GetByCarIdAsync(
        string carId,
        int currentPage,
        int pageSize,
        CancellationToken cancellationToken = default);
    Task<IEnumerable<Deal>> GetByUserIdAsync(
        string userId,
        int currentPage,
        int pageSize,
        CancellationToken cancellationToken = default);
}