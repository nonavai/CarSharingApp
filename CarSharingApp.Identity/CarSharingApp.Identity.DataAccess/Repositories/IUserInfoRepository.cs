using CarSharingApp.Identity.DataAccess.Entities;
using CarSharingApp.Identity.DataAccess.Specifications;

namespace CarSharingApp.Identity.DataAccess.Repositories;

public interface IUserInfoRepository 
{
    Task<UserInfo> AddAsync(UserInfo entity, CancellationToken token = default);
    Task<UserInfo?> GetByIdAsync(string id, CancellationToken token = default);
    Task<IEnumerable<UserInfo>> GetBySpecAsync(UserInfoSpecification spec, CancellationToken token = default);
    Task<UserInfo> UpdateAsync(UserInfo entity, CancellationToken token = default);
    Task DeleteAsync(UserInfo entity, CancellationToken token = default);
    Task<UserInfo?> GetByUserIdAsync(string userId, CancellationToken token = default);
}