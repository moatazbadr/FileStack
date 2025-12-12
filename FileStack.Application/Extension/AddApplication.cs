using Microsoft.Extensions.DependencyInjection;

namespace FileStack.Application.Extension;

public static class AddApplication
{
    public static void AddApplicationLayer(this IServiceCollection services)
    {
        // Add Application Layer Services Here
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(AddApplication).Assembly));
    }

}
