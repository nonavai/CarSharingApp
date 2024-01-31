using CarSharingApp.Identity.BusinessLogic.Models.User;
using CarSharingApp.Identity.BusinessLogic.Models.UserInfo;

namespace CarSharingApp.Identity.BusinessLogic.Services;

public interface IUserManageService
{
    Task<UserCleanDto> LogInAsync(LogInDto dto);
    Task RegistrationAsync(UserNecessaryDto dto);
    Task<UserDto> GetByIdAsync(string id);
    Task<IEnumerable<UserCleanDto>> GetByNameAsync(string firstName, string lastName, CancellationToken token = default);
    Task<UserNecessaryDto> UpdateAsync(string id, UserNecessaryDto dto);
    Task<UserCleanDto> DeleteAsync(string id);
    Task<IEnumerable<UserInfoDto>> GetExpiredUserInfosAsync(CancellationToken token = default);
    Task<UserInfoDto> AddUserInfoAsync(string userId, UserInfoCleanDto dto, CancellationToken token = default);
    Task<UserInfoCleanDto> UpdateUserInfoAsync(string id, UserInfoCleanDto dto);
    Task<UserInfoCleanDto> DeleteUserInfoAsync(string id, CancellationToken token = default);
}