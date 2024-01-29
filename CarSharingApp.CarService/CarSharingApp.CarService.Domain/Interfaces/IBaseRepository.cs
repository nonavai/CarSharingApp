namespace CarSharingApp.CarService.Domain.Interfaces;

public interface IBaseRepository<T>
{
    Task<T?> GetByIdAsync(Guid id);
    Task<IEnumerable<T>> GetAllAsync();
    Task<T> AddAsync(T entity);
    Task<T> UpdateAsync(T entity);
    Task<T?> DeleteAsync(Guid id);
    Task<bool> ExistsAsync(Guid id);
}