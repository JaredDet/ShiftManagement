using ShiftManagement.Api.Modules.Dev.Application;

namespace ShiftManagement.Api.Modules.Dev;

public static class DevModule
{
    public static IServiceCollection AddODev(this IServiceCollection services)
    {
        services.AddScoped<DevBootstrapUseCase>();

        return services;
    }
}