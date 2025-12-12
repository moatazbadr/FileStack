using FileStack.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FileStack.Infrastructure.Extensions;

public static class AddInfrastructureExtenstion
{
    public static void AddInfrastructure (this IServiceCollection Services , IConfiguration configuration)
    {
        Services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
        });
        
        

    }
}
