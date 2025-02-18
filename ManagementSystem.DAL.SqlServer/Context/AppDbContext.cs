﻿using ManagementSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ManagementSystem.DAL.SqlServer.Context;
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions options) : base(options)
    {

    }

    public DbSet<Category> Categories { get; set; }
    public DbSet<Customer> Customers { get; set; }
}