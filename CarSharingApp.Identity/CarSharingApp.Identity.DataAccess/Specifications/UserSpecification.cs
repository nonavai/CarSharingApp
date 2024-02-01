using System.Linq.Expressions;
using CarSharingApp.Identity.DataAccess.Entities;

namespace CarSharingApp.Identity.DataAccess.Specifications;

public class UserSpecification : BaseSpecification<User>
{
    public UserSpecification(Expression<Func<User, bool>> criteria) : base(criteria)
    {
    }
    public UserSpecification WithUserInfo()
    {
        Includes.Add(u => u.UserInfo);
        return this;
    }
}