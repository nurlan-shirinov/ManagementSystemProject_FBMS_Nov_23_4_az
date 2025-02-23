using ManagementSystem.Common.Exceptions;
using ManagementSystem.DAL.SqlServer.Context;
using ManagementSystem.Domain.Entities;
using ManagementSystem.Repository.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ManagementSystem.DAL.SqlServer.Infrastructure;

public class SqlUserRepository(AppDbContext context) : IUserRepository
{
    private readonly AppDbContext _context = context;

    public IQueryable<User> GetAll()
    {
        return _context.Users.Where(u => u.IsDeleted == false);
    }

    public async Task<User> GetByEmailAsync(string email)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Email == email && u.IsDeleted == false);
    }

    public async Task<User> GetByIdAsync(int id)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Id == id && u.IsDeleted == false);
    }

    public async Task RegisterAsync(User user)
    {
        user.CreatedDate = DateTime.Now;
        user.CreatedBy = 1;
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
    }

    public void Remove(int id)
    {
        var currentUser = _context.Users.FirstOrDefault(u => u.Id == id);
        if (currentUser == null)
        {
            throw new BadRequestException("User Tapilmadi");
        }
        currentUser.IsDeleted = true;
        currentUser.DeletedDate = DateTime.Now;
        currentUser.DeletedBy = 1;
        _context.Users.Update(currentUser);
        _context.SaveChanges();
    }

    public void Update(User user)
    {
        user.UpdatedDate = DateTime.Now;
        user.UpdatedBy = 1;
        _context.Users.Update(user);
    }
}