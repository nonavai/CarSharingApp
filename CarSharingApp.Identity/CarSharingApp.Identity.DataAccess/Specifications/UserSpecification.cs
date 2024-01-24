using System.Linq.Expressions;
using CarSharingApp.Identity.DataAccess.Entities;
using CarSharingApp.Identity.DataAccess.Specifications.SpecSettings;

namespace CarSharingApp.Identity.DataAccess.Specifications;

public class UserSpecification : ISpecification<User>
{
    public UserSpecification(Expression<Func<User, bool>> criteria)
    {
        Criteria = criteria;

    }
    public Expression<Func<User, bool>> Criteria { get; }
    public List<Expression<Func<User, object>>> Includes { get; } = new();
    
    public UserSpecification WithUserInfo()
    {
        Includes.Add(u => u.UserInfo);
        return this;
    }
}