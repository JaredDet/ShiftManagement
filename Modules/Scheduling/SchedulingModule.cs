using ShiftManagement.Api.Modules.Scheduling.Application;
using ShiftManagement.Api.Modules.Scheduling.Infrastructure;

namespace ShiftManagement.Api.Modules.Scheduling;

public static class SchedulingModule
{
    public static IServiceCollection AddScheduling(this IServiceCollection services)
    {
        services.AddScoped<ShiftRepository>();

        services.AddScoped<CreateShiftUseCase>();

        return services;
    }
}