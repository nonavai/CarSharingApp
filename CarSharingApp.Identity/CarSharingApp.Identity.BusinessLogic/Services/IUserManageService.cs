using CarSharingApp.Identity.BusinessLogic.Models.User;
using CarSharingApp.Identity.BusinessLogic.Models.UserInfo;
using CarSharingApp.Identity.Shared.Enums;

namespace CarSharingApp.Identity.BusinessLogic.Services;

public interface IUserManageService
{
    Task<UserCleanDto> LogInAsync(LogInDto dto);
    Task RegistrationAsync(UserNecessaryDto dto, CancellationToken token = default);
    Task<UserDto> GetByIdAsync(string id);
    Task<IEnumerable<UserCleanDto>> GetByNameAsync(string firstName, string lastName, CancellationToken token = default);
    Task<UserNecessaryDto> UpdateAsync(string id, UserNecessaryDto dto);
    Task<UserCleanDto> DeleteAsync(string id, CancellationToken token = default);
    Task<IEnumerable<UserInfoDto>> GetExpiredUserInfosAsync(CancellationToken token = default);
    Task<UserInfoDto> AddUserInfoAsync(string userId, UserInfoCleanDto dto, CancellationToken token = default);
    Task<UserInfoCleanDto> UpdateUserInfoAsync(string id, UserInfoCleanDto dto);
}