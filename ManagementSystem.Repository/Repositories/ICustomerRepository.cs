using ManagementSystem.Domain.Entities;

namespace ManagementSystem.Repository.Repositories;

public interface ICustomerRepository
{
    Task AddAsync(Customer customer);
}