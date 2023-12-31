using DataAccess.Entities;

namespace DataAccess.Repositories;

public interface IRefreshTokenRepository : IBaseRepository<RefreshToken>
{
    Task<RefreshToken?> GetByUserId(int id);
}