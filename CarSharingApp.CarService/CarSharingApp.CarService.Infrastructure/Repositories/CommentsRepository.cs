using CarSharingApp.CarService.Application.Repositories;
using CarSharingApp.CarService.Domain.Entities;
using CarSharingApp.CarService.Infrastructure.DataBase;

namespace CarSharingApp.CarService.Infrastructure.Repositories;

public class CommentsRepository : BaseRepository<Comment>, ICommentRepository
{
    private CarsContext db;

    public CommentsRepository(CarsContext db) : base(db)
    {
        this.db = db;
    }

    public async Task<IEnumerable<Comment>> GetByCarIdAsync(string id)
    {
        return db.Comments.Where(f => f.CarId == id).AsEnumerable();
    }
}