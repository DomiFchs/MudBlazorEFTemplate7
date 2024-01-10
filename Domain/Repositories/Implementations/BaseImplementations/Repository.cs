using System.Linq.Expressions;
using Domain.Extensions;
using Domain.Repositories.Interfaces;
using Domain.Repositories.Interfaces.BaseInterfaces;
using Microsoft.EntityFrameworkCore;
using Model.Configuration;
using MudBlazor;

namespace Domain.Repositories.Implementations.BaseImplementations;

public class Repository<TEntity> : ACommonRepository<TEntity>, IRepository<TEntity> where TEntity : class {

    protected Repository(IDbContextFactory<CustomDbContext> contextFactory) : base(contextFactory) { }
    
    public virtual async Task<TEntity?> ReadAsync(int id, CancellationToken ct) {
        await using var context = await ContextFactory.CreateDbContextAsync(ct);
        var table = context.Set<TEntity>();
        return await table.FindAsync(new object[] { id }, ct);
    }

    public async Task DeleteAsync(int id, CancellationToken ct) {
        await using var context = await ContextFactory.CreateDbContextAsync(ct);
        var table = context.Set<TEntity>();
        var entity = await table.FindAsync(new object[] { id }, ct);
        if (entity == null) return;
        table.Remove(entity);
        await context.SaveChangesAsync(ct);
    }

    public async Task<bool> ExistsAsync(int id, CancellationToken ct) {
        await using var context = await ContextFactory.CreateDbContextAsync(ct);
        var table = context.Set<TEntity>();
        return await table.FindAsync(new object[] { id }, ct) != null;
    }
}