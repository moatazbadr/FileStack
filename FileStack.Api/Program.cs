
using FileStack.Api.Extension;
using FileStack.Application.Extension;
using FileStack.Application.Interfaces;
using FileStack.Infrastructure.Extensions;
using FileStack.Infrastructure.Repositories;

namespace FileStack.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddPresentationLayer(builder.Configuration);
            builder.Services.AddInfrastructure(builder.Configuration);
            builder.Services.AddApplicationLayer();
            //builder.Services.AddScoped<IauthService, AuthService>();
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
