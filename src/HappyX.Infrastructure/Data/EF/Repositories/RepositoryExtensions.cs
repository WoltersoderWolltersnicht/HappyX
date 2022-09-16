using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace HappyX.Infrastructure.Data.EF.Repositories;

internal static class RepositoryExtensions
{
    public static IBaseRepository<TEntity> GetRepository<TEntity>(this IServiceProvider serviceProvider, DbContext context)
    {
        IBaseRepository<TEntity> repository = serviceProvider.GetRequiredService<IBaseRepository<TEntity>>();
        
        repository.InitDbContext(context);
        
        return repository;
    }
}