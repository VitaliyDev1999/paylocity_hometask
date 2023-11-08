using Application.Behaviors;
using Application.Features.Dependent.CreateDependent;
using FluentValidation;
using MediatR;
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


        // Register the ValidationBehavior with MediatR.
        serviceCollection.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        serviceCollection.AddTransient<IValidator<CreateDependentCommand>, CreateDependentCommandValidator>();

        return serviceCollection;
    }
}

