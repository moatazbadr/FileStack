using FileStack.Api.Middlewares;
using FileStack.Application.Interfaces;
using FileStack.Application.Validators;
using FileStack.Domain.Entities;
using FileStack.Infrastructure.JWT;
using FileStack.Infrastructure.Persistence;
using FileStack.Infrastructure.Repositories;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace FileStack.Api.Extension
{
    public static class AddPresentation
    {
        public static void AddPresentationLayer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication();
            services.AddIdentity<ApplicationUser, IdentityRole>().
                AddEntityFrameworkStores<ApplicationDbContext>();
            // Add Presentation Layer Services Here
            services.Configure<JWTConfiguration>(configuration.GetSection("JWTConfiguration"));
           

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }
           )
             .AddJwtBearer(o =>
               {
                   o.RequireHttpsMetadata = false; //in production should be true because we want to use https  
                   o.SaveToken = false;
                   o.TokenValidationParameters = new TokenValidationParameters
                   {
                       ValidateIssuerSigningKey = true,
                       ValidateIssuer = true,
                       ValidateAudience = true,
                       ValidateLifetime = true,
                       ValidIssuer = configuration["JWTConfiguration:Issuer"],
                       ValidAudience = configuration["JWTConfiguration:Audience"],
                       IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(configuration["JWTConfiguration:Key"]))


                   };
               })
               ;
            services.AddScoped<IauthService,AuthService>();
            services.AddTransient<ItokenHandler, tokenHandler>();
            
            services.AddTransient(
 typeof(IPipelineBehavior<,>),
 typeof(ValidationBehavior<,>));
        }


    }
    }

