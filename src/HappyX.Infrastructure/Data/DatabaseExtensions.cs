using HappyX.Infrastructure.Data.EF;
using HappyX.Infrastructure.Data.Mock;
using HappyX.Infrastructure.Data.UnitOfWork;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace HappyX.Infrastructure.Data;

public static class DatabaseExtensions
{
    public static IServiceCollection AddDatabase(this IServiceCollection serviceCollection,
        DatabaseOptions databaseOptions)
    {
        serviceCollection.AddTransient<WorkUnit>();

        switch (databaseOptions.Type)
        {
            case "Postgres": serviceCollection.AddPostgres(databaseOptions);
                break;
            case "Test": 
                serviceCollection.AddMocks(databaseOptions);
                serviceCollection.Replace(ServiceDescriptor.Transient<WorkUnit, MockWorkUnit>());
                break;
        }
        
        return serviceCollection;
    }
}