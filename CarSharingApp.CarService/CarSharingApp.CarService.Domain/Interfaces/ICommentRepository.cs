using CarSharingApp.CarService.Domain.Entities;

namespace CarSharingApp.CarService.Domain.Interfaces;

public interface ICommentRepository : IBaseRepository<Comment>
{
    Task<IEnumerable<Comment>> GetByCarIdAsync(string id);
}