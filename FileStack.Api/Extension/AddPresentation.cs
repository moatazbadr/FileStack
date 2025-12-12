using FileStack.Application.Interfaces;
using FileStack.Domain.Entities;
using FileStack.Infrastructure.JWT;
using FileStack.Infrastructure.Persistence;
using FileStack.Infrastructure.Repositories;
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
            services.AddSwaggerGen(c =>
            {
                c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = Microsoft.OpenApi.Models.ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
                });
                c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
                {
                    {
                        new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                        {
                            Reference = new Microsoft.OpenApi.Models.OpenApiReference
                            {
                                Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] {}
                    }
                });
            });

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


        }
    }
}
