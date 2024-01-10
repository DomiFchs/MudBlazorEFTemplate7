using System.Linq.Expressions;
using MudBlazor;

namespace Domain.Repositories.Interfaces.BaseInterfaces;

public interface ICommonRepository<TEntity> where TEntity : class
    {
        Task<List<TEntity>> ReadAsync(Expression<Func<TEntity, bool>> filter, CancellationToken ct);
        Task<List<TEntity>> ReadAsync(CancellationToken ct);
        Task<List<TEntity>> ReadAsync(CancellationToken ct, params Expression<Func<TEntity, object>>[] includes);
        Task<List<TEntity>> ReadAsync(Expression<Func<TEntity, bool>> filter, CancellationToken ct, params Expression<Func<TEntity, object>>[] includes);
        Task<List<TEntity>> SearchAsync<TProperty>(Expression<Func<TEntity, TProperty>> propertySelector, string? search, int skip, int take, CancellationToken ct, params (Expression<Func<TEntity, object>>, SortDirection)[] sort);
        Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> filter, CancellationToken ct);
        Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> filter, CancellationToken ct);
        Task<TEntity> CreateAsync(TEntity entity, CancellationToken ct);
        Task<List<TEntity>> CreateAsync(List<TEntity> entities, CancellationToken ct);
        Task UpdateAsync(TEntity entity, CancellationToken ct);
        Task UpdateAsync(IEnumerable<TEntity> entities, CancellationToken ct);
        Task DeleteAsync(TEntity entity, CancellationToken ct);
        Task DeleteAsync(IEnumerable<TEntity> entities, CancellationToken ct);
        Task DeleteAsync(Expression<Func<TEntity, bool>> filter, CancellationToken ct);
        Task<int> CountAsync(Expression<Func<TEntity, bool>> filter, CancellationToken ct);
        Task<int> CountAsync(CancellationToken ct);
        Task<int> CountAsync(CancellationToken ct, params Expression<Func<TEntity, object>>[] includes);
        Task<int> CountAsync(Expression<Func<TEntity, bool>> filter, CancellationToken ct, params Expression<Func<TEntity, object>>[] includes);
        Task<int> CountAsync<TProperty>(Expression<Func<TEntity, TProperty>> propertySelector, string? search, CancellationToken ct);
    }