using ManagementSystem.Domain.Entities;

namespace ManagementSystem.Repository.Repositories;

public interface IRefreshTokenRepository
{
    Task SaveRefreshToken(RefreshToken refreshToken);
    Task<RefreshToken> GetRefreshToken(string token);
}