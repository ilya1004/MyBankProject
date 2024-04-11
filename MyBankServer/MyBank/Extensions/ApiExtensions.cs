using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using MyBank.API.Interfaces;
using MyBank.Application.Utils;

namespace MyBank.API.Extensions;

public static class ApiExtensions
{
    public static IServiceCollection AddApiAuthentication(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        var jwtOptions = configuration.GetSection(nameof(JwtOptions)).Get<JwtOptions>();

        services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(
                JwtBearerDefaults.AuthenticationScheme,
                options =>
                {
                    options.TokenValidationParameters = new()
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(jwtOptions!.SecretKey)
                        )
                    };

                    options.Events = new JwtBearerEvents
                    {
                        OnMessageReceived = context =>
                        {
                            context.Token = context.Request.Cookies["my-cookie"];
                            return Task.CompletedTask;
                        }
                    };
                }
            );
        return services;
    }

    public static IServiceCollection AddApiAuthorization(this IServiceCollection services)
    {
        services
            .AddAuthorizationBuilder()
            .AddPolicy(
                AuthorizationPolicies.UserPolicy,
                policy =>
                {
                    policy.RequireClaim("Role", "User");
                }
            )
            .AddPolicy(
                AuthorizationPolicies.ModeratorPolicy,
                policy =>
                {
                    policy.RequireClaim("Role", "Moderator");
                }
            )
            .AddPolicy(
                AuthorizationPolicies.AdminPolicy,
                policy =>
                {
                    policy.RequireClaim("Role", "Admin");
                }
            )
            .AddPolicy(
                AuthorizationPolicies.UserAndModeratorPolicy,
                policy =>
                {
                    policy.RequireClaim("Role", "User", "Moderator");
                }
            )
            .AddPolicy(
                AuthorizationPolicies.ModeratorAndAdminPolicy,
                policy =>
                {
                    policy.RequireClaim("Role", "Moderator", "Admin");
                }
            )
            .AddPolicy(
                AuthorizationPolicies.UserAndAdminPolicy,
                policy =>
                {
                    policy.RequireClaim("Role", "User", "Admin");
                }
            )
            .AddPolicy(
                AuthorizationPolicies.UserAndModeratorAndAdminPolicy,
                policy =>
                {
                    policy.RequireClaim("Role", "User", "Moderator", "Admin");
                }
            );

        return services;
    }

    public static IServiceCollection AddApiUtils(this IServiceCollection services)
    {
        services.AddScoped<ICookieValidator, CookieValidator>();
        return services;
    }
}
