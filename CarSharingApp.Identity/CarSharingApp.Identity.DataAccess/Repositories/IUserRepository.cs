using CarSharingApp.Identity.DataAccess.Entities;
using CarSharingApp.Identity.DataAccess.Specifications;
using Microsoft.AspNetCore.Identity;

namespace CarSharingApp.Identity.DataAccess.Repositories;

public interface IUserRepository 
{
    Task<User?> GetByEmailAsync(string email);
    Task<IdentityResult> AddAsync(User entity, string password, CancellationToken token);
    Task<bool> CheckPasswordAsync(User entity, string password);
    Task<IEnumerable<string>> GetRolesAsync(User user);
    Task<IdentityResult> AddToRoleAsync(User user, string role);
    Task<IdentityResult> RemoveFromRolesAsync(User user, string role, CancellationToken token);
    Task<User?> GetByIdAsync(string id);
    Task<IEnumerable<User>> GetBySpecAsync(UserSpecification spec, CancellationToken token);
    Task<IdentityResult> UpdateAsync(User entity);
    Task<IdentityResult> DeleteAsync(User entity, CancellationToken token);
}