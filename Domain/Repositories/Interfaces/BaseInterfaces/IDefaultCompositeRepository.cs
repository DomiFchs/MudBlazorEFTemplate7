namespace Domain.Repositories.Interfaces.BaseInterfaces;

public interface IDefaultCompositeRepository<TEntity> : ICompositeKeyRepository<TEntity, int, int> where TEntity : class {
    
}