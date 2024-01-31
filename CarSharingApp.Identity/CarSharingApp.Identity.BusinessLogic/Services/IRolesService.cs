using CarSharingApp.Identity.Shared.Enums;

namespace CarSharingApp.Identity.BusinessLogic.Services;

public interface IRolesService
{
    Task<IEnumerable<string>> GetUserRolesAsync(string id);
    Task AddUserRoleAsync(string id, Roles role);
    Task RemoveUserRoleAsync(string id, Roles role);
}