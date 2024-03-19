using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using MyBank.Application.Utils;
using System.Text;

namespace MyBank.API.Extensions;

public static class ApiExtensions
{
    public static IServiceCollection AddApiAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        var jwtOptions = configuration.GetSection(nameof(JwtOptions)).Get<JwtOptions>();

        services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
            {
                options.TokenValidationParameters = new()
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions!.SecretKey))
                };

                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        context.Token = context.Request.Cookies["my-cookie"];
                        return Task.CompletedTask;
                    }
                };
            });
        return services;
    }

    public static IServiceCollection AddApiAuthorization(this IServiceCollection services)
    {
        services.AddAuthorizationBuilder()
            .AddPolicy("UserPolicy", policy =>
            {
                policy.RequireClaim("Role", "User");
            })
            .AddPolicy("ModeratorPolicy", policy =>
            {
                policy.RequireClaim("Role", "Moderator");
            })
            .AddPolicy("AdminPolicy", policy =>
            {
                policy.RequireClaim("Role", "Admin");
            })
            .AddPolicy("UserAndModeratorPolicy", policy =>
            {
                policy.RequireClaim("Role", "User", "Moderator");
            })
            .AddPolicy("ModeratorAndAdminPolicy", policy =>
            {
                policy.RequireClaim("Role", "Moderator", "Admin");
            })
            .AddPolicy("UserAndAdminPolicy", policy =>
            {
                policy.RequireClaim("Role", "User", "Admin");
            })
            .AddPolicy("UserAndModeratorAndAdminPolicy", policy =>
            {
                policy.RequireClaim("Role", "User", "Moderator", "Admin");
            });

        return services;
    }
}
