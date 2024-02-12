using CarSharingApp.CarService.Application.Repositories;
using CarSharingApp.CarService.Domain.Entities;
using CarSharingApp.CarService.Infrastructure.DataBase;

namespace CarSharingApp.CarService.Infrastructure.Repositories;

public class CommentRepository : BaseRepository<Comment>, ICommentRepository
{
    private CarsContext _dataBase;

    public CommentRepository(CarsContext dataBase) : base(dataBase)
    {
        _dataBase = dataBase;
    }

    public async Task<IEnumerable<Comment>> GetByCarIdAsync(string id)
    {
        return _dataBase.Comments.Where(f => f.CarId == id).AsEnumerable();
    }
}