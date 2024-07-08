using FRM.Application.Interfaces;
using FRM.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace FRM.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IMessageService, MessageService>();

        return services;
    }
}
