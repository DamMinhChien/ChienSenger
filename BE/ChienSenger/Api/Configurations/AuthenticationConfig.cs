using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Api.Configurations;

public static class AuthenticationConfig
{
    public static IServiceCollection AddAuthenticationWithJwtBearer(this IServiceCollection services, IConfiguration config)
    {
        services.AddAuthentication("Bearer").AddJwtBearer("Bearer", options =>
        {
            var jwtSettings = config.GetSection("JwtSettings");
            options.TokenValidationParameters = new TokenValidationParameters()
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                RequireExpirationTime = true,
                RequireSignedTokens = true,
                ValidIssuer = config["Issuer"],
                ValidAudience = config["Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["SecretKey"]!))
            };
        });
        return services;
    }
}