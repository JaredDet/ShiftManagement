using ShiftManagement.Api.Modules.Scheduling.Application.Shifts;
using ShiftManagement.Api.Modules.Scheduling.Application.Swaps;
using ShiftManagement.Api.Modules.Scheduling.Infrastructure;

namespace ShiftManagement.Api.Modules.Scheduling;

public static class SchedulingModule
{
    public static IServiceCollection AddScheduling(this IServiceCollection services)
    {
        services.AddScoped<ShiftRepository>();
        services.AddScoped<ShiftAssignmentRepository>();
        services.AddScoped<ShiftSwapRequestRepository>();

        services.AddScoped<CreateShiftUseCase>();
        services.AddScoped<UpdateShiftUseCase>();
        services.AddScoped<AssignCollaboratorToShiftUseCase>();
        services.AddScoped<ReplaceCollaboratorInShiftUseCase>();

        services.AddScoped<ApproveShiftSwapUseCase>();
        services.AddScoped<CancelShiftSwapUseCase>();
        services.AddScoped<RequestShiftSwapUseCase>();
        services.AddScoped<RespondToShiftSwapUseCase>();

        return services;
    }
}