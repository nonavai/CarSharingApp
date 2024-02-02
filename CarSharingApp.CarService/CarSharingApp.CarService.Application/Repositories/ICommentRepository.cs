using CarSharingApp.CarService.Domain.Entities;

namespace CarSharingApp.CarService.Application.Repositories;

public interface ICommentRepository : IBaseRepository<Comment>
{
    Task<IEnumerable<Comment>> GetByCarIdAsync(string id);
}