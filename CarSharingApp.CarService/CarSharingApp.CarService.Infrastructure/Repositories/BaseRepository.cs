using CarSharingApp.CarService.Domain.Entities;
using CarSharingApp.CarService.Domain.Interfaces;
using CarSharingApp.CarService.Infrastructure.DataBase;
using Microsoft.EntityFrameworkCore;

namespace CarSharingApp.CarService.Infrastructure.Repositories;

public abstract class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
{
    private CarsContext db;

    protected BaseRepository(CarsContext db)
    {
        this.db = db;
    }

    public virtual async Task<T?> GetByIdAsync(string id)
    {
        return await db.Set<T>().FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return db.Set<T>().AsNoTracking().AsEnumerable();
    }

    public async Task<T> AddAsync(T entity)
    {
        await db.Set<T>().AddAsync(entity);
        await db.SaveChangesAsync();
        return entity;
    }

    public async Task<T> UpdateAsync(T entity)
    {
        db.Entry(entity).State = EntityState.Modified;
        await db.SaveChangesAsync();
        return entity;
    }

    public async Task<T?> DeleteAsync(string id)
    {
        var entity = await GetByIdAsync(id);
        if (entity != null)
        {
            db.Set<T>().Remove(entity);
            await db.SaveChangesAsync();
            return entity;
        }

        return null;
    }

    public async Task<bool> ExistsAsync(string id)
    {
        return await db.Set<T>().AsNoTracking().AnyAsync(p => p.Id == id);
    }
}