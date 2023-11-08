using Microsoft.Extensions.DependencyInjection;

namespace Application.Extention;

public static class ConfigurationExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddAutoMapper(typeof(IApplicationAssemblyMarker));

        serviceCollection.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssemblyContaining(typeof(IApplicationAssemblyMarker));
        });

        return serviceCollection;
    }
}

