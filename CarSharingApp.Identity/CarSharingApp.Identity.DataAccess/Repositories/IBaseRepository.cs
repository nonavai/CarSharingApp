using CarSharingApp.Identity.DataAccess.Specifications.SpecSettings;

namespace CarSharingApp.Identity.DataAccess.Repositories;

public interface IBaseRepository<T, M> where M: ISpecification<T>
{
    Task<T?> GetByIdAsync(string id);
    Task<IEnumerable<T>> GetBySpecAsync(M spec);
    Task UpdateAsync(T entity);
    Task DeleteAsync(T entity);
}