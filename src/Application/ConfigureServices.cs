using Application.Commands;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class ConfigureServices
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<ProductCommand>();
        services.AddScoped<OrderCommand>();

        return services;
    }
}