using CarSharingApp.Identity.BusinessLogic.Models.Role;

namespace CarSharingApp.Identity.BusinessLogic.Services;

public interface IRolesService
{
    Task<IEnumerable<RoleDto>> GetUserRolesAsync(string id);
    Task AddUserRoleAsync(string id, string role);
    Task RemoveUserRoleAsync(string id, string role);
}