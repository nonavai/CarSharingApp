using CarSharingApp.DealService.DataAccess.DataBase;
using CarSharingApp.DealService.DataAccess.Entities;
using MongoDB.Driver;

namespace CarSharingApp.DealService.DataAccess.Repositories.Implementations;

public class AnswerRepository : BaseRepository<Answer>, IAnswerRepository 
{
    
    public AnswerRepository(MongoContext context) : base(context, "Answers")
    {
    }

    public async Task<IEnumerable<Answer>> GetByFeedBackIdAsync(string feedBackId, int currentPage, int pageSize, CancellationToken cancellationToken = default)
    {
        return await _collection.Find(answer => answer.FeedBackId == feedBackId)
            .SortBy(answer => answer.Posted)
            .Skip((currentPage - 1) * pageSize)
            .Limit(pageSize)
            .ToListAsync(cancellationToken: cancellationToken);
    }

    public async Task DeleteByFeedbackAsync(string feedbackId, CancellationToken cancellationToken = default)
    {
        await _collection.DeleteManyAsync(answer => answer.FeedBackId == feedbackId, cancellationToken: cancellationToken);
    }
}