using CarSharingApp.Identity.DataAccess.DbContext;
using CarSharingApp.Identity.DataAccess.Entities;
using CarSharingApp.Identity.DataAccess.Specifications;
using CarSharingApp.Identity.DataAccess.Specifications.SpecSettings;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CarSharingApp.Identity.DataAccess.Repositories.Implementation;

public class UserRepository :  IUserRepository
{
    private CarSharingContext db;
    private UserManager<User> _userManager;

    public UserRepository(CarSharingContext db, UserManager<User> userManager)
    {
        this.db = db;
        _userManager = userManager;
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        return await _userManager.FindByEmailAsync(email);
    }

    public async Task<User?> GetByIdAsync(string id)
    {
        return await _userManager.FindByIdAsync(id);
    }

    public async Task<IEnumerable<User>> GetBySpecAsync(UserSpecification spec, CancellationToken token = default)
    {
        IQueryable<User> query = db.Users;
        query = query.ApplySpecification(spec);
        
        return await query.ToListAsync(cancellationToken: token);
    }

    public async Task<IdentityResult> AddAsync(User entity, string password)
    {
        return await _userManager.CreateAsync(entity, password);
    }

    public async Task<bool> CheckPasswordAsync(User entity, string password)
    {
        return await _userManager.CheckPasswordAsync(entity, password);
    }

    public async Task<IdentityResult> UpdateAsync(User entity)
    {
        return await _userManager.UpdateAsync(entity);
    }

    public async Task<IdentityResult> DeleteAsync(User entity)
    {
        return await _userManager.DeleteAsync(entity);
    }

    public async Task<IEnumerable<string>> GetRolesAsync(User user)
    {
        return await _userManager.GetRolesAsync(user);
    } 
    
    public async Task<IdentityResult> AddToRoleAsync(User user, string role)
    {
        return await _userManager.AddToRoleAsync(user, role);
    }

    public async Task<IdentityResult> RemoveFromRolesAsync(User user, string role)
    {
        return await _userManager.RemoveFromRoleAsync(user, role);
    }
    
}