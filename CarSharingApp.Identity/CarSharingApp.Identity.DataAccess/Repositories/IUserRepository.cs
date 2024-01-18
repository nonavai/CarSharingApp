using CarSharing.UserService.DataAccess.Helpers;
using CarSharingApp.Identity.DataAccess.Entities;
using Microsoft.AspNetCore.Identity;

namespace CarSharingApp.Identity.DataAccess.Repositories;

public interface IUserRepository : IBaseRepository<User, UserSpecification>
{
    Task<User?> GetByEmailAsync(string email);
    Task<IdentityResult> AddAsync(User entity, string password);
}