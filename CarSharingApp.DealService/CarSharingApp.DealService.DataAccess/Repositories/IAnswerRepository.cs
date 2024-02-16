using CarSharingApp.DealService.DataAccess.Entities;

namespace CarSharingApp.DealService.DataAccess.Repositories;

public interface IAnswerRepository : IBaseRepository<Answer>
{
    Task<IEnumerable<Answer>> GetByFeedBackIdAsync(string feedBackId, int currentPage, int pageSize, CancellationToken cancellationToken);
}