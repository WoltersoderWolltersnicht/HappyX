using HappyX.Domain.Internal;
using HappyX.Infrastructure.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;
namespace HappyX.Infrastructure.Data.Mock;

internal static class MockExtensions
{
    public static void AddMocks(this IServiceCollection serviceCollection,
        DatabaseOptions databaseOptions)
    {
        serviceCollection.AddMockRepositories();
    }
    
    private static IServiceCollection AddMockRepositories(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddTransient<IBaseRepository<User>, MockRepository<User>>();
        serviceCollection.AddTransient<IBaseRepository<Mood>, MockRepository<Mood>>();
        serviceCollection.AddTransient<IBaseRepository<Record>, MockRepository<Record>>();

        return serviceCollection;
    }
}