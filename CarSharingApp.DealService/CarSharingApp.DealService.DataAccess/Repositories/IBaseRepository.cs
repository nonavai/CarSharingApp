using System.Linq.Expressions;

namespace CarSharingApp.DealService.DataAccess.Repositories;

public interface IBaseRepository<T>
{
    Task<IEnumerable<T>> GetAllAsync(int currentPage, int pageSize, CancellationToken cancellationToken = default);
    Task<T> GetByIdAsync(string id, CancellationToken cancellationToken = default);
    Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> filter, CancellationToken cancellationToken = default);
    Task<T> CreateAsync(T entity, CancellationToken cancellationToken = default);
    Task UpdateAsync(string id, T entity, CancellationToken cancellationToken = default);
    Task DeleteAsync(string id, CancellationToken cancellationToken = default);
}