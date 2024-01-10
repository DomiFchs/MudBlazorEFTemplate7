using System.Linq.Expressions;
using Domain.Extensions;
using Domain.Repositories.Interfaces.BaseInterfaces;
using Microsoft.EntityFrameworkCore;
using Model.Configuration;
using MudBlazor;

namespace Domain.Repositories.Implementations.BaseImplementations;

public class ACompositeKeyRepository<TEntity, TKey1, TKey2> : ACommonRepository<TEntity>,ICompositeKeyRepository<TEntity, TKey1, TKey2> where TEntity : class {
    
    
    
    public async Task<TEntity?> ReadAsync(TKey1? key1, TKey2? key2, CancellationToken ct)
    {
        await using var context = await ContextFactory.CreateDbContextAsync(ct);
        var table = context.Set<TEntity>();
        if (key1 == null || key2 == null) return null;
        return await table.FindAsync(new object[] { key1, key2 }, ct);
    }

    public async Task DeleteAsync(TKey1? key1, TKey2? key2, CancellationToken ct) {
        await using var context = await ContextFactory.CreateDbContextAsync(ct);
        var table = context.Set<TEntity>();
        if (key1 == null || key2 == null) return;
        var entity = await table.FindAsync(new object[] { key1, key2 }, ct);
        if (entity == null) return;
        table.Remove(entity);
        await context.SaveChangesAsync(ct);
    }

    public async Task<bool> ExistsAsync(TKey1? key1, TKey2? key2, CancellationToken ct) {
        await using var context = await ContextFactory.CreateDbContextAsync(ct);
        var table = context.Set<TEntity>();
        if (key1 == null || key2 == null) return false;
        return await table.FindAsync(new object[] { key1, key2 }, ct) != null;
    }


    protected ACompositeKeyRepository(IDbContextFactory<CustomDbContext> contextFactory) : base(contextFactory) { }
}