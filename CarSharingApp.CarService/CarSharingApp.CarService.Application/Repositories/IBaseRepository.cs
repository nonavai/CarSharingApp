namespace CarSharingApp.CarService.Application.Repositories;

public interface IBaseRepository<T>
{
    Task<T?> GetByIdAsync(string id);
    Task<IEnumerable<T>> GetAllAsync();
    Task<T> AddAsync(T entity);
    Task<T> UpdateAsync(T entity);
    Task<T?> DeleteAsync(string id);
    Task<bool> ExistsAsync(string id);
}