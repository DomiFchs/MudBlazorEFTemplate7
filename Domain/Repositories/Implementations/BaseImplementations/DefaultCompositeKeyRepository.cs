using Domain.Repositories.Interfaces.BaseInterfaces;
using Microsoft.EntityFrameworkCore;
using Model.Configuration;

namespace Domain.Repositories.Implementations.BaseImplementations;

public class DefaultCompositeKeyRepository<TEntity> : ACompositeKeyRepository<TEntity, int, int>, IDefaultCompositeRepository<TEntity> where TEntity : class {
    protected DefaultCompositeKeyRepository(IDbContextFactory<CustomDbContext> contextFactory) : base(contextFactory) { }
}