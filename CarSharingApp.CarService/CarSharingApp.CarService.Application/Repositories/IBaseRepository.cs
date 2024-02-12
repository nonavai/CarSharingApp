namespace CarSharingApp.CarService.Application.Repositories;

public interface IBaseRepository<T>
{
    Task<T?> GetByIdAsync(string id, CancellationToken token = default);
    Task<IEnumerable<T>> GetAllAsync();
    Task<T> AddAsync(T entity, CancellationToken token = default);
    Task<T> UpdateAsync(T entity, CancellationToken token = default);
    Task<T?> DeleteAsync(string id, CancellationToken token = default);
    Task SaveChangesAsync(CancellationToken token = default);
    Task<bool> ExistsAsync(string id, CancellationToken token = default);
}