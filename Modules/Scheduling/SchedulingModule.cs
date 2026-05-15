using ShiftManagement.Api.Modules.Scheduling.Application.Shifts;
using ShiftManagement.Api.Modules.Scheduling.Infrastructure;

namespace ShiftManagement.Api.Modules.Scheduling;

public static class SchedulingModule
{
    public static IServiceCollection AddScheduling(this IServiceCollection services)
    {
        services.AddScoped<ShiftRepository>();
        services.AddScoped<ShiftAssignmentRepository>();

        services.AddScoped<CreateShiftUseCase>();
        services.AddScoped<UpdateShiftUseCase>();
        services.AddScoped<AssignCollaboratorToShiftUseCase>();
        services.AddScoped<ReplaceCollaboratorInShiftUseCase>();

        return services;
    }
}