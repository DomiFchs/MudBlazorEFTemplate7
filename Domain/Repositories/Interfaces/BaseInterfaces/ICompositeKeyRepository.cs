using System.Linq.Expressions;
using MudBlazor;

namespace Domain.Repositories.Interfaces.BaseInterfaces;

 public interface ICompositeKeyRepository<TEntity, in TKey1, in TKey2> : ICommonRepository<TEntity> where TEntity : class
    {
        Task<TEntity?> ReadAsync(TKey1? key1, TKey2? key2, CancellationToken ct);
        Task DeleteAsync(TKey1? key1, TKey2? key2, CancellationToken ct);
        Task<bool> ExistsAsync(TKey1? key1, TKey2? key2, CancellationToken ct);
    }