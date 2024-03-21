using System.Linq.Expressions;
using CarSharingApp.Identity.DataAccess.Specifications.SpecSettings;

namespace CarSharingApp.Identity.DataAccess.Specifications;

public abstract class BaseSpecification<T> : ISpecification<T>
{
    public BaseSpecification(Expression<Func<T, bool>> criteria)
    {
        Criteria = criteria;

    }
    public Expression<Func<T, bool>> Criteria { get; }
    public List<Expression<Func<T, object>>> Includes { get; } = new ();
}