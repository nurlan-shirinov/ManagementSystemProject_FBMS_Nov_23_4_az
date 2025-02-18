using ManagementSystem.DAL.SqlServer.Context;
using ManagementSystem.Domain.Entities;
using ManagementSystem.Repository.Repositories;

namespace ManagementSystem.DAL.SqlServer.Infrastructure;

public class SqlCustomerRepository : ICustomerRepository
{
    private readonly AppDbContext _appDbContext;
    public SqlCustomerRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task AddAsync(Customer customer)
    {
        await _appDbContext.AddAsync(customer);
        await _appDbContext.SaveChangesAsync();
    }
}