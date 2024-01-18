using System.Linq.Expressions;
using CarSharingApp.Identity.DataAccess.Entities;
using CarSharingApp.Identity.DataAccess.Specifications.SpecSettings;

namespace CarSharingApp.Identity.DataAccess.Specifications;

public class UserInfoSpecification  : ISpecification<UserInfo>
{
    
    public UserInfoSpecification(Expression<Func<UserInfo, bool>> criteria)
    {
        Criteria = criteria;

    }
    public Expression<Func<UserInfo, bool>> Criteria { get; }
    public List<Expression<Func<UserInfo, object>>> Includes { get; } = new();
    
    public UserInfoSpecification WithUser()
    {
        Includes.Add(u => u.User);
        return this;
    }
    
}