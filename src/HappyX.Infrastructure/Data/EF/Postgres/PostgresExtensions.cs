using HappyX.Domain.Internal;
using HappyX.Infrastructure.Data.EF.Repositories;
using HappyX.Infrastructure.Data.EF.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace HappyX.Infrastructure.Data.EF.Postgres;

public static class PostgresExtensions
{
    public static void AddPostgres(this IServiceCollection serviceCollection,
        DatabaseOptions databaseOptions)
    {
        serviceCollection.AddDbContext<HappyXContext>(options =>
            options.UseNpgsql($"Host=localhost;Database=HappyX;Username=postgres;Password=postgres", 
                optionsBuilder => optionsBuilder.MigrationsAssembly("HappyX.Api")));
        
        //serviceCollection.AddPostgresRepositories();
        //serviceCollection.AddTransient<WorkUnit>();
    }
    
    public static IServiceCollection AddPostgresRepositories(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddTransient<IBaseRepository<User>, PostgresRepository<User>>();
        serviceCollection.AddTransient<IBaseRepository<Mood>, PostgresRepository<Mood>>();
        serviceCollection.AddTransient<IBaseRepository<Record>, PostgresRepository<Record>>();

        return serviceCollection;
    }
}