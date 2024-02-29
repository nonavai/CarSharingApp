using CarSharingApp.DealService.DataAccess.Entities;

namespace CarSharingApp.DealService.DataAccess.Repositories;

public interface IFeedBackRepository : IBaseRepository<Feedback>
{
    Task<IEnumerable<Feedback>> GetByDealIdAsync(
        string dealId,
        int currentPage,
        int pageSize,
        CancellationToken cancellationToken = default);
}