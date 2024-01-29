using CarSharingApp.Identity.DataAccess.DbContext;
using CarSharingApp.Identity.DataAccess.Entities;
using CarSharingApp.Identity.DataAccess.Specifications;
using CarSharingApp.Identity.DataAccess.Specifications.SpecSettings;
using Microsoft.EntityFrameworkCore;

namespace CarSharingApp.Identity.DataAccess.Repositories.Implementation;

public class UserInfoRepository :  IUserInfoRepository
{
    private CarSharingContext db;

    public UserInfoRepository(CarSharingContext db)
    {
        this.db = db;
    }

    public async Task<UserInfo?> GetByIdAsync(string id)
    {
        return await db.UserInfos.AsNoTracking().FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task<IEnumerable<UserInfo>> GetBySpecAsync(UserInfoSpecification spec, CancellationToken token)
    {
        token.ThrowIfCancellationRequested();
        
        IQueryable<UserInfo> query = db.UserInfos;
        query = query.ApplySpecification(spec);
        return await query.ToListAsync();
    }

    public async Task<UserInfo> UpdateAsync(UserInfo entity)
    {
        db.Entry(entity).State = EntityState.Modified;
        await db.SaveChangesAsync();
        return entity;
    }

    public async Task DeleteAsync(UserInfo entity, CancellationToken token)
    {
        token.ThrowIfCancellationRequested();
        
        db.UserInfos.Remove(entity);
        await db.SaveChangesAsync();
    }

    public async Task<UserInfo> AddAsync(UserInfo entity, CancellationToken token = default)
    {
        token.ThrowIfCancellationRequested();
        
        await db.UserInfos.AddAsync(entity);
        await db.SaveChangesAsync();
        return entity;
    }
    
    public async Task<UserInfo?> GetByUserId(string userId)
    {
        return await db.UserInfos.AsNoTracking().FirstOrDefaultAsync(ui => ui.UserId == userId);
    }
}