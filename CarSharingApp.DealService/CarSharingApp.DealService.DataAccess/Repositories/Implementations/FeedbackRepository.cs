using CarSharingApp.DealService.DataAccess.DataBase;
using CarSharingApp.DealService.DataAccess.Entities;
using MongoDB.Driver;

namespace CarSharingApp.DealService.DataAccess.Repositories.Implementations;

public class FeedbackRepository : BaseRepository<Feedback>, IFeedBackRepository
{
    public FeedbackRepository(MongoContext context) : base(context, "Feedbacks")
    {
    }

    public async Task<IEnumerable<Feedback>> GetByDealIdAsync(string dealId, int currentPage, int pageSize, CancellationToken cancellationToken = default)
    {
        return await _collection.Find(feedback => feedback.DealId == dealId)
            .SortBy(answer => answer.Posted)
            .Skip((currentPage - 1) * pageSize)
            .Limit(pageSize)
            .ToListAsync(cancellationToken: cancellationToken);;
    }
}