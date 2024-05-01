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

    public async Task<UserInfo?> GetByIdAsync(string id, CancellationToken token = default)
    {
        return await db.UserInfos.AsNoTracking().FirstOrDefaultAsync(u => u.Id == id, cancellationToken: token);
    }

    public async Task<IEnumerable<UserInfo>> GetBySpecAsync(UserInfoSpecification spec, CancellationToken token)
    {
        IQueryable<UserInfo> query = db.UserInfos;
        query = query.ApplySpecification(spec);
        
        return await query.ToListAsync(cancellationToken: token);
    }

    public async Task<UserInfo> UpdateAsync(UserInfo entity, CancellationToken token = default)
    {
        db.Entry(entity).State = EntityState.Modified;
        await db.SaveChangesAsync(token);
        
        return entity;
    }

    public async Task DeleteAsync(UserInfo entity, CancellationToken token)
    {
        db.UserInfos.Remove(entity);
        await db.SaveChangesAsync(token);
    }

    public async Task<UserInfo> AddAsync(UserInfo entity, CancellationToken token = default)
    {
        await db.UserInfos.AddAsync(entity, token);
        await db.SaveChangesAsync(token);
        
        return entity;
    }
    
    public async Task<UserInfo?> GetByUserIdAsync(string userId, CancellationToken token = default)
    {
        return await db.UserInfos.AsNoTracking().FirstOrDefaultAsync(ui => ui.UserId == userId, cancellationToken: token);
    }
}