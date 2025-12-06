using Application.Common.Interfaces;
using Application.Friends.Interfaces.Repositories;
using Application.Users.Interfaces.Repositories;
using Application.Users.Interfaces.Security;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Repositories;
using Infrastructure.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        // DbContext
        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
        });
        
        // Unit of work
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        
        // Hasher
        services.AddSingleton<IPasswordHasher, PasswordHasher>();
        
        // Token
        services.AddSingleton<ITokenService, TokenService>();
        
        // Repositories
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUserStatusRepository, UserStatusRepository>();
        services.AddScoped<IFriendRepository, FriendRepository>();
        
        return services;
    }
}