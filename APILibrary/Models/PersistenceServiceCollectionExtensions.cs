using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace APILibrary.Models;

/// <summary>
/// Class PersistenceServiceCollectionExtensions.
/// </summary>
public static class PersistenceServiceCollectionExtensions
{
    /// <summary>
    /// Adds the persistence.
    /// </summary>
    /// <param name="services">The services.</param>
    /// <param name="configuration">The configuration.</param>
    /// <returns>IServiceCollection.</returns>
    public static IServiceCollection AddPersistence(this IServiceCollection services, 
        IConfiguration configuration)
    {
        var connectionString = configuration["ConnectionStrings:DbConnection"];

        services.AddDbContext<UsersContext>(options =>
        {
            options.UseNpgsql(connectionString);
        });

        services.AddScoped<IUserContext>(provider => provider.GetService<UsersContext>()!);
        return services;
    }
}