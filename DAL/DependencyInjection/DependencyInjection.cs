using DAL.ApplicationValidator;
using Domain.Entity;
using Domain.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DAL.DependencyInjection;

public static class DependencyInjection
{
    public static void AddAccessLayer(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("PostgresSQL");
        serviceCollection.AddDbContext<ApplicationDbContext>(o => o.UseNpgsql(connectionString));
        serviceCollection.InitRepo();
    }

    private static void InitRepo(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IApplicationRepository, ApplicationRepository.ApplicationRepository>();
    }
}