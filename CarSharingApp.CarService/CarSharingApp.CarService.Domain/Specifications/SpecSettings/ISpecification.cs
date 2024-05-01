using System.Linq.Expressions;

namespace CarSharingApp.CarService.Domain.Specifications.SpecSettings;

public interface ISpecification<T> 
{
    Expression<Func<T, bool>> Criteria { get; }
    List<Expression<Func<T, object>>> Includes { get; }
}