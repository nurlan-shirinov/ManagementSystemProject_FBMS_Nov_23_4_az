﻿using Dapper;
using ManagementSystem.DAL.SqlServer.Context;
using ManagementSystem.Domain.Entities;
using ManagementSystem.Repository.Repositories;

namespace ManagementSystem.DAL.SqlServer.Infrastructure;

public class SqlCategoryRepository : BaseSqlRepository, ICategoryRepository
{
    private readonly AppDbContext _context;
    public SqlCategoryRepository(string connectionString, AppDbContext context) : base(connectionString)
    {
        _context = context;
    }

    public async Task AddAsync(Category category)
    {
        var sql = @"INSERT INTO Categories ([Name],[CreatedBy])
                    VALUES(@Name , @CreatedBy); SELECT SCOPE_IDENTITY()";

        using var conn = OpenConnection();
        var generatedId = await conn.ExecuteScalarAsync<int>(sql, category);
        category.Id = generatedId;
    }

    public async Task<bool> Delete(int id, int deletedBy)
    {
        var checkSql = @"SELECT Id From Categories WHERE Id = @id and IsDeleted=0";

        var sql = $@"UPDATE Categories
                     SET IsDeleted = 1,
                     DeletedBy = @deletedBy,
                     DeletedDate = GETDATE()
                     WHERE Id = @id";

        using var conn = OpenConnection();
        using var transaction = conn.BeginTransaction();

        var categoryId = await conn.ExecuteScalarAsync<int?>(checkSql, new { id }, transaction);

        if (!categoryId.HasValue)
            return false;

        var affectedRows = await conn.ExecuteAsync(sql, new { id, deletedBy }, transaction);
        transaction.Commit();
        return affectedRows > 0;
    }

    public IQueryable<Category> GetAll()
    {
        return _context.Categories.OrderByDescending(c => c.CreatedDate).Where(c => c.IsDeleted == false);
    }

    public async Task<IEnumerable<Category>> GetAllInitialDataAsync()
    {
        var sql = @"SELECT C.[Id] , C[Name]
                    FROM Categories AS C
                    WHERE IsDeleted = 0";

        using var conn = OpenConnection();
        return await conn.QueryAsync<Category>(sql, conn);
    }

    public async Task<Category> GetByIdAsync(int id)
    {
        var sql = @"SELECT C.*
                    FROM Categories AS C
                    WHERE C.Id = @id AND C.IsDeleted=0";

        using var con = OpenConnection();
        return await con.QueryFirstOrDefaultAsync<Category>(sql, new { id });

    }

    public void Update(Category category)
    {
        var sql = @"UPDATE Categories
                    SET Name = @Name,
                    UpdatedBy = @UpdatedBy,
                    UpdatedDate = GETDATE()
                    WHERE Id = @Id";

        using var conn = OpenConnection();
        conn.Query(sql, category);
    }
}