
using FileStack.Api.Extension;
using FileStack.Api.Middlewares;
using FileStack.Application.Extension;
using FileStack.Application.Interfaces;
using FileStack.Infrastructure.Extensions;
using FileStack.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace FileStack.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddPresentationLayer(builder.Configuration);
            builder.Services.AddInfrastructure(builder.Configuration);
            builder.Services.AddApplicationLayer();
            builder.Services.AddScoped<ServerErrorMiddleWare>();
            //builder.Services.AddScoped<IauthService, AuthService>();
            builder.Host.UseSerilog((context, config) => {
                config.ReadFrom.Configuration(context.Configuration);
            });
            var app = builder.Build();
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseMiddleware<ServerErrorMiddleWare>();
            app.UseSerilogRequestLogging();
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
