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

    public async Task<IEnumerable<UserInfo>> GetBySpecAsync(UserInfoSpecification spec)
    {
        IQueryable<UserInfo> query = db.UserInfos;
        query = query.ApplySpecification(spec);
        return await query.ToListAsync();
    }

    public async Task UpdateAsync(UserInfo entity)
    {
        db.Entry(entity).State = EntityState.Modified;
        await db.SaveChangesAsync();
    }

    public async Task DeleteAsync(UserInfo entity)
    {
        db.UserInfos.Remove(entity);
    }

    public async Task<UserInfo> AddAsync(UserInfo entity)
    {
        await db.UserInfos.AddAsync(entity);
        await db.SaveChangesAsync();
        return entity;
    }
}