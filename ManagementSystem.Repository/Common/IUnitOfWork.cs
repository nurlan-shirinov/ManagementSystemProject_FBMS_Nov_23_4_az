﻿using ManagementSystem.Repository.Repositories;

namespace ManagementSystem.Repository.Common;

public interface IUnitOfWork
{
    public ICategoryRepository CategoryRepository { get; }
    public ICustomerRepository CustomerRepository { get; }
    public IUserRepository UserRepository { get; }
    public IRefreshTokenRepository RefreshTokenRepository { get; }
    Task<int> SaveChange();
}