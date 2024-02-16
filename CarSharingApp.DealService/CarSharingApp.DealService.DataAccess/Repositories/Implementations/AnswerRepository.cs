using CarSharingApp.DealService.DataAccess.DataBase;
using CarSharingApp.DealService.DataAccess.Entities;
using MongoDB.Driver;

namespace CarSharingApp.DealService.DataAccess.Repositories.Implementations;

public class AnswerRepository : BaseRepository<Answer>, IAnswerRepository 
{
    
    public AnswerRepository(IMongoContext context) : base(context, "answer")
    {
        
    }

    public async Task<IEnumerable<Answer>> GetByFeedBackIdAsync(string feedBackId, int currentPage, int pageSize, CancellationToken cancellationToken)
    {
        return await _collection.Find(answer => answer.FeedBackId == feedBackId)
            .SortBy(answer => answer.Posted)
            .Skip((currentPage - 1) * pageSize)
            .Limit(pageSize)
            .ToListAsync(cancellationToken: cancellationToken);
    }
}