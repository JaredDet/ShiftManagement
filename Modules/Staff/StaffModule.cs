using ShiftManagement.Api.Modules.Staff.Application.Collaborators;
using ShiftManagement.Api.Modules.Staff.Application.EmploymentAssignments;
using ShiftManagement.Api.Modules.Staff.Application.Positions;
using ShiftManagement.Api.Modules.Staff.Infrastructure;




namespace ShiftManagement.Api.Modules.Staff;

public static class StaffModule
{
    public static IServiceCollection AddStaff(this IServiceCollection services)
    {
        services.AddScoped<CreateCollaboratorUseCase>();
        services.AddScoped<GetCollaboratorUseCase>();
        services.AddScoped<ListCollaboratorsUseCase>();
        services.AddScoped<DeactivateCollaboratorUseCase>();

        services.AddScoped<CreatePositionUseCase>();
        services.AddScoped<UpdatePositionUseCase>();
        services.AddScoped<GetPositionUseCase>();
        services.AddScoped<ListPositionsUseCase>();
        services.AddScoped<DeactivatePositionUseCase>();

        services.AddScoped<AssignBranchToCollaboratorUseCase>();
        services.AddScoped<RemoveBranchFromCollaboratorUseCase>();
        services.AddScoped<AssignPositionToCollaboratorUseCase>();
        services.AddScoped<RemovePositionFromCollaboratorUseCase>();
        services.AddScoped<ChangeMainBranchUseCase>();
        services.AddScoped<ChangeMainPositionUseCase>();

        services.AddScoped<CollaboratorRepository>();
        services.AddScoped<CollaboratorReadRepository>();
        services.AddScoped<PositionRepository>();
        services.AddScoped<PositionReadRepository>();

        return services;
    }
}