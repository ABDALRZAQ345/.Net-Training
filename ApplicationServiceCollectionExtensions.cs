using WebApplication2.Database;
using WebApplication2.Repositories;

namespace WebApplication2;

public static class ApplicationServiceCollectionExtensions
{

    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddSingleton<IMovieRepository, MovieRepository>();
        return services;
    }

    public static IServiceCollection AddDatabase(this IServiceCollection services,
        string connectionString)
    {
        services.AddSingleton<IDbConnectionFactory>(_ => new NpgsqlConnectionFactory(connectionString));
       services.AddSingleton<DbInitializer>();
        return services;
    }
    
    
}