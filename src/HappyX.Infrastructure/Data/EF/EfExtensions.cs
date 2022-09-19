using HappyX.Domain.Internal;
using HappyX.Infrastructure.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace HappyX.Infrastructure.Data.EF;

internal static class EfExtensions
{
    public static void AddPostgres(this IServiceCollection serviceCollection,
        DatabaseOptions databaseOptions)
    {
        string connectionString =
            $"Host={databaseOptions.Host};Database={databaseOptions.Database};Username={databaseOptions.Username};Password={databaseOptions.Password}";

        serviceCollection.AddDbContext<HappyXContext>(
            options =>
                options.UseNpgsql(connectionString,
                    optionsBuilder => optionsBuilder.MigrationsAssembly("HappyX.Api")));

        serviceCollection.AddEfRepositories();
    }

    private static IServiceCollection AddEfRepositories(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddTransient<IBaseRepository<User>, EfRepository<User>>();
        serviceCollection.AddTransient<IBaseRepository<Mood>, EfRepository<Mood>>();
        serviceCollection.AddTransient<IBaseRepository<Record>, EfRepository<Record>>();

        return serviceCollection;
    }
}