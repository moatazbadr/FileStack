using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;

namespace FileStack.Application.Extension;

public static class AddApplication
{
    public static void AddApplicationLayer(this IServiceCollection services)
    {
        // Add Application Layer Services Here
        var assembly = typeof(AddApplication).Assembly; 

        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(AddApplication).Assembly));
        services.AddValidatorsFromAssembly(assembly)
           .AddFluentValidationAutoValidation()
           ;
    }

}
