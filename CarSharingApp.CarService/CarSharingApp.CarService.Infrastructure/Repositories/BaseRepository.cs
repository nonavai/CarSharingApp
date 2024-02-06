using CarSharingApp.CarService.Application.Repositories;
using CarSharingApp.CarService.Domain.Entities;
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

    public virtual async Task<T?> GetByIdAsync(string id, CancellationToken token = default)
    {
        return await db.Set<T>().FirstOrDefaultAsync(p => p.Id == id, token);
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return db.Set<T>().AsNoTracking().AsEnumerable();
    }

    public async Task<T> AddAsync(T entity, CancellationToken token = default)
    {
        await db.Set<T>().AddAsync(entity, token);
        await db.SaveChangesAsync(token);
        return entity;
    }

    public async Task<T> UpdateAsync(T entity, CancellationToken token = default)
    {
        db.Entry(entity).State = EntityState.Modified;
        await db.SaveChangesAsync(token);
        return entity;
    }

    public async Task<T?> DeleteAsync(string id, CancellationToken token = default)
    {
        var entity = await GetByIdAsync(id, token);
        if (entity != null)
        {
            db.Set<T>().Remove(entity);
            await db.SaveChangesAsync(token);
            return entity;
        }

        return null;
    }

    public async Task<bool> ExistsAsync(string id, CancellationToken token = default)
    {
        return await db.Set<T>().AsNoTracking().AnyAsync(p => p.Id == id, cancellationToken: token);
    }
}