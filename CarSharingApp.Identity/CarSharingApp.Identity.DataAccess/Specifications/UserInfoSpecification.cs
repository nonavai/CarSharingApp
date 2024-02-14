using System.Linq.Expressions;
using CarSharingApp.Identity.DataAccess.Entities;

namespace CarSharingApp.Identity.DataAccess.Specifications;

public class UserInfoSpecification  : BaseSpecification<UserInfo>
{
    public UserInfoSpecification(Expression<Func<UserInfo, bool>> criteria) : base(criteria)
    {
    }
    
    public UserInfoSpecification WithUser()
    {
        Includes.Add(u => u.User);
        return this;
    }
}