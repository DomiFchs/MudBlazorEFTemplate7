using System.Linq.Expressions;
using MudBlazor;

namespace Domain.Repositories.Interfaces.BaseInterfaces;

public interface IRepository<TEntity> : ICommonRepository<TEntity> where TEntity : class {
    Task<TEntity?> ReadAsync(int id, CancellationToken ct);
    Task DeleteAsync(int id, CancellationToken ct);
    Task<bool> ExistsAsync(int id, CancellationToken ct);
}