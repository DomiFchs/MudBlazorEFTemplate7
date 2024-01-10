using System.Linq.Expressions;
using Domain.Extensions;
using Domain.Repositories.Interfaces.BaseInterfaces;
using Microsoft.EntityFrameworkCore;
using Model.Configuration;
using MudBlazor;

namespace Domain.Repositories.Implementations.BaseImplementations;

public class ACommonRepository<TEntity> : ICommonRepository<TEntity> where TEntity : class{
    
    protected readonly IDbContextFactory<CustomDbContext> ContextFactory;

    protected ACommonRepository(IDbContextFactory<CustomDbContext> contextFactory) {
        ContextFactory = contextFactory;
    }
    
    public virtual async Task<List<TEntity>> ReadAsync(CancellationToken ct) {
        await using var context = await ContextFactory.CreateDbContextAsync(ct);
        var table = context.Set<TEntity>();
        return await table.AsNoTracking().ToListAsync(ct);
        
    }
    
     public virtual async Task<List<TEntity>> ReadAsync(Expression<Func<TEntity, bool>> filter, CancellationToken ct) {
        await using var context = await ContextFactory.CreateDbContextAsync(ct);
        var table = context.Set<TEntity>();
        return await table.AsNoTracking().Where(filter).ToListAsync(ct);
    }

    public virtual async Task<List<TEntity>> ReadAsync(CancellationToken ct,
        params Expression<Func<TEntity, object>>[] includes) {
        
        await using var context = await ContextFactory.CreateDbContextAsync(ct);
        var table = context.Set<TEntity>();
        var query = table.AsNoTracking().IgnoreAutoIncludes();
        foreach (var include in includes) {
            query = query.Include(include);
        }
        return await query.ToListAsync(ct);
    }
    
    public virtual async Task<List<TEntity>> ReadAsync(Expression<Func<TEntity, bool>> filter, CancellationToken ct,
        params Expression<Func<TEntity, object>>[] includes) {
        
        await using var context = await ContextFactory.CreateDbContextAsync(ct);
        var table = context.Set<TEntity>();
        var query = table.AsNoTracking().IgnoreAutoIncludes().Where(filter);
        foreach (var include in includes) {
            query = query.Include(include);
        }
        return await query.ToListAsync(ct);
    }
    
    public async Task<List<TEntity>> SearchAsync<TProperty>(Expression<Func<TEntity, TProperty>> propertySelector,
        string? search, int skip, int take, CancellationToken ct,
        params (Expression<Func<TEntity, object>>, SortDirection)[] sort) {
        
        ValidateParams(skip, take);

        await using var context = await ContextFactory.CreateDbContextAsync(ct);
        var table = context.Set<TEntity>();

        var query = table.AsQueryable();
        if (!search.IsNullEmptyOrWhiteSpace()) query = SearchFor(propertySelector, search);
        return await query
            .AsNoTracking()
            .OrderByMultiple(sort)
            .SkipTake(skip, take)
            .ToListAsync(ct);
    }
    
    protected IQueryable<TEntity> SearchFor<TProperty>(Expression<Func<TEntity, TProperty>> propertySelector,
        string? search) {
        ArgumentNullException.ThrowIfNull(propertySelector);
        ArgumentNullException.ThrowIfNull(search);

        using var context = ContextFactory.CreateDbContext();
        var table = context.Set<TEntity>();
        return table.AsNoTracking().Where(CreateLikeExpression(propertySelector, search));
    }

    protected static Expression<Func<TEntity, bool>> CreateLikeExpression<TProperty>(
        Expression<Func<TEntity, TProperty>> propertySelector, string search) {
        var property = propertySelector.Body;

        var likeMethod = typeof(DbFunctionsExtensions).GetMethod(nameof(DbFunctionsExtensions.Like),
            new[] { typeof(DbFunctions), typeof(string), typeof(string) });
        var functionsInstance = Expression.Constant(EF.Functions);
        var searchExpression = Expression.Constant($"%{search}%");
        var likeCall = Expression.Call(likeMethod ?? throw new InvalidOperationException(),
            functionsInstance, property, searchExpression);

        var lambda = Expression.Lambda<Func<TEntity, bool>>(likeCall, propertySelector.Parameters);

        return lambda;
    }
    
    protected virtual void ValidateParams(int skip, int take) {
        if (skip < 0 || take <= 0)
            throw new ArgumentOutOfRangeException(nameof(skip) + " or " + nameof(take) + " is less than or equal to 0");
    }


    public virtual async Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> filter,
        CancellationToken ct) {
        await using var context = await ContextFactory.CreateDbContextAsync(ct);
        var table = context.Set<TEntity>();
        return await table.AsNoTracking().FirstOrDefaultAsync(filter, ct);
    }

    public async Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> filter, CancellationToken ct) {
        await using var context = await ContextFactory.CreateDbContextAsync(ct);
        var table = context.Set<TEntity>();
        return await table.AsNoTracking().IgnoreAutoIncludes().AnyAsync(filter, ct);
    }

    public virtual async Task<TEntity> CreateAsync(TEntity entity, CancellationToken ct) {
        await using var context = await ContextFactory.CreateDbContextAsync(ct);
        var table = context.Set<TEntity>();
        await table.AddAsync(entity, ct);
        await context.SaveChangesAsync(ct);
        return entity;
    }

    public virtual async Task<List<TEntity>> CreateAsync(List<TEntity> entity, CancellationToken ct) {
        await using var context = await ContextFactory.CreateDbContextAsync(ct);
        var table = context.Set<TEntity>();
        await table.AddRangeAsync(entity, ct);
        await context.SaveChangesAsync(ct);
        return entity;
    }

    public virtual async Task UpdateAsync(TEntity entity, CancellationToken ct) {
        await using var context = await ContextFactory.CreateDbContextAsync(ct);
        var table = context.Set<TEntity>();
        table.Update(entity);
        await context.SaveChangesAsync(ct);
    }

    public virtual async Task UpdateAsync(IEnumerable<TEntity> entity, CancellationToken ct) {
        await using var context = await ContextFactory.CreateDbContextAsync(ct);
        var table = context.Set<TEntity>();
        table.UpdateRange(entity);
        await context.SaveChangesAsync(ct);
    }

    public virtual async Task DeleteAsync(TEntity entity, CancellationToken ct) {
        await using var context = await ContextFactory.CreateDbContextAsync(ct);
        var table = context.Set<TEntity>();
        table.Remove(entity);
        await context.SaveChangesAsync(ct);
    }

    public virtual async Task DeleteAsync(IEnumerable<TEntity> entities, CancellationToken ct) {
        await using var context = await ContextFactory.CreateDbContextAsync(ct);
        var table = context.Set<TEntity>();
        table.RemoveRange(entities);
        await context.SaveChangesAsync(ct);
    }

    public virtual async Task DeleteAsync(Expression<Func<TEntity, bool>> filter, CancellationToken ct) {
        await using var context = await ContextFactory.CreateDbContextAsync(ct);
        var table = context.Set<TEntity>();
        var entities = await table.AsNoTracking().IgnoreAutoIncludes().Where(filter).ToListAsync(ct);

        table.RemoveRange(entities);
        await context.SaveChangesAsync(ct);
    }
    
    public virtual async Task<int> CountAsync(Expression<Func<TEntity, bool>> filter, CancellationToken ct) {
        await using var context = await ContextFactory.CreateDbContextAsync(ct);
        var table = context.Set<TEntity>();
        return await table.AsNoTracking().IgnoreAutoIncludes().CountAsync(filter, ct);
    }
    
    public virtual async Task<int> CountAsync(CancellationToken ct) {
        await using var context = await ContextFactory.CreateDbContextAsync(ct);
        var table = context.Set<TEntity>();
        return await table.AsNoTracking().IgnoreAutoIncludes().CountAsync(ct);
    }
    public virtual async Task<int> CountAsync<TProperty>(Expression<Func<TEntity, TProperty>> propertySelector, string? search,
        CancellationToken ct) {
        
        var context = await ContextFactory.CreateDbContextAsync(ct);
        var table = context.Set<TEntity>();
        var query = table.AsQueryable();
        if (!search.IsNullEmptyOrWhiteSpace()) query = SearchFor(propertySelector, search);
        return await query
            .IgnoreAutoIncludes()
            .AsNoTracking()
            .CountAsync(ct);
    }
}