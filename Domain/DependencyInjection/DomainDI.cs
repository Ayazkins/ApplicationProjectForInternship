using DAL.ApplicationValidator;
using Domain.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Domain.DependencyInjection;

public static class DomainDI
{
    public static void AddDi(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IApplicationService, ApplicationService>();
        serviceCollection.AddScoped<UnsubmittedApplicationValidator, UnsubmittedApplicationValidator>();
        serviceCollection.AddScoped<ApplicationValidator, ApplicationValidator>();
    }
}