using CarSharingApp.Identity.BusinessLogic.Models.User;
using CarSharingApp.Identity.BusinessLogic.Models.UserInfo;
using CarSharingApp.Identity.Shared.Enums;

namespace CarSharingApp.Identity.BusinessLogic.Services;

public interface IUserManageService
{
    Task<UserDto> LogInAsync(LogInDto dto);
    Task RegistrationAsync(UserNecessaryDto dto, CancellationToken token);
    Task<UserDto> GetByIdAsync(string id);
    Task<IEnumerable<UserCleanDto>> GetByNameAsync(string firstName, string lastName, CancellationToken token);
    Task<UserNecessaryDto> UpdateAsync(string id, UserNecessaryDto dto);
    Task<UserCleanDto> DeleteAsync(string id, CancellationToken token);
    Task<IEnumerable<string>> GetUserRolesAsync(string id);
    Task AddUserRoleAsync(string id, Roles role);
    Task RemoveUserRoleAsync(string id, Roles role, CancellationToken token);
    Task<IEnumerable<UserInfoDto>> GetExpiredUserInfosAsync(CancellationToken token);
    Task<UserInfoDto> AddUserInfoAsync(string userId, UserInfoCleanDto dto, CancellationToken token);
    Task<UserInfoCleanDto> UpdateUserInfoAsync(string id, UserInfoCleanDto dto);
}