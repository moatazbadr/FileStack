using FileStack.Infrastructure.MailService;
using FileStack.Infrastructure.MailService.MailSettings;
using FileStack.Infrastructure.Persistence;
using FileStack.Infrastructure.Repositories;
using MailKit;
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
        Services.Configure<MailSettingsHelper>(configuration.GetSection("MailSettings"));
        Services.AddTransient<IMailingService,MailingSerivce>();

        Services.AddScoped<IUserProfileService,UserProfileService>();
        Services.AddScoped<IStorageRepository,StorageRepository>();


    }
}
