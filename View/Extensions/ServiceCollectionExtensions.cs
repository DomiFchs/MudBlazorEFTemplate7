using Domain.Repositories.Implementations.BaseImplementations;
using Domain.Repositories.Interfaces.BaseInterfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;

namespace View.Extensions;

public static class ServiceCollectionExtensions {
    
    public static IServiceCollection AddDefaultDbContextOfType<TDbContext>(this IServiceCollection services, string? connectionString) where TDbContext : DbContext {
        services.AddDbContextFactory<TDbContext>(
            options =>
                options.UseMySql(
                    connectionString,
                    new MySqlServerVersion(new Version(8, 0, 31))
                ).UseLoggerFactory(new NullLoggerFactory()),
            ServiceLifetime.Transient
        );
        
        return services;
    }
    
    public static IServiceCollection AddSingleKeyRepository<TEntity>(this IServiceCollection services) where TEntity : class {
        services.AddTransient<IRepository<TEntity>, Repository<TEntity>>();
        return services;
    }
    
    public static IServiceCollection AddDefaultCompositeKeyRepository<TEntity>(this IServiceCollection services) where TEntity : class {
        services.AddTransient<IDefaultCompositeRepository<TEntity>, DefaultCompositeKeyRepository<TEntity>>();
        return services;
    }
    
    public static IServiceCollection AddCompositeKeyRepository<TEntity, TKey1, TKey2>(this IServiceCollection services) where TEntity : class {
        services.AddTransient<ICompositeKeyRepository<TEntity, TKey1, TKey2>, ACompositeKeyRepository<TEntity, TKey1, TKey2>>();
        return services;
    }
}