using CarSharingApp.Identity.DataAccess.Entities;
using CarSharingApp.Identity.DataAccess.Specifications;

namespace CarSharingApp.Identity.DataAccess.Repositories;

public interface IUserInfoRepository : IBaseRepository<UserInfo, UserInfoSpecification>
{
    Task<UserInfo> AddAsync(UserInfo entity);
}