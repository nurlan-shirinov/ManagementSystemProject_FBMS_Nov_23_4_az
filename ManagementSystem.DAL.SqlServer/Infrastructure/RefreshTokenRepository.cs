using ManagementSystem.DAL.SqlServer.Context;
using ManagementSystem.Domain.Entities;
using ManagementSystem.Repository.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ManagementSystem.DAL.SqlServer.Infrastructure;

public class RefreshTokenRepository(AppDbContext appDbContext) : IRefreshTokenRepository
{
    private readonly AppDbContext _appDbContext = appDbContext;

    public async Task<RefreshToken> GetRefreshToken(string token)
    {
        return await _appDbContext.RefreshTokens.FirstOrDefaultAsync(t=>t.Token==token);
    }

    public async Task SaveRefreshToken(RefreshToken refreshToken)
    {
        await _appDbContext.RefreshTokens.AddAsync(refreshToken);
    }
}