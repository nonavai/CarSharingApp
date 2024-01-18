using CarSharing.UserService.DataAccess.Helpers;
using CarSharingApp.Identity.DataAccess.DbContext;
using CarSharingApp.Identity.DataAccess.Entities;
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
    

    public async Task<IEnumerable<User>> GetBySpecAsync(UserSpecification spec)
    {
        IQueryable<User> query = db.Users;
        query = query.ApplySpecification(spec);
        return await query.ToListAsync();
    }

    public async Task<IdentityResult> AddAsync(User entity, string password)
    {
        return await _userManager.CreateAsync(entity, password);
    }

    public async Task UpdateAsync(User entity)
    {
        await _userManager.UpdateAsync(entity);
        
    }

    public async Task DeleteAsync(User entity)
    {
        await _userManager.DeleteAsync(entity);
    }
}