using ShiftManagement.Api.Modules.Staff.Api.Contracts.PositionAssignments;
using ShiftManagement.Api.Modules.Staff.Infrastructure;
using ShiftManagement.Api.Modules.Staff.Application.Errors;
using ShiftManagement.Api.Modules.Staff.Domain;
using ShiftManagement.Api.Infrastructure.Persistence;
using ShiftManagement.Api.BuildingBlocks.Results;

namespace ShiftManagement.Api.Modules.Staff.Application.EmploymentAssignments;

public sealed class AssignPositionToCollaboratorUseCase(
    CollaboratorRepository CollaboratorRepository,
    PositionRepository positionRepository,
    ShiftManagementDbContext context
)
{
    public async Task<Result> Execute(Guid collaboratorId, AssignPositionToCollaboratorRequest request)
    {
        var collaborator = await CollaboratorRepository.GetByIdAsync(collaboratorId);

        if (collaborator is null)
            return Result.Failure(StaffErrors.CollaboratorNotFound);

        var exists = await positionRepository.ExistsAsync(request.PositionId);

        if (!exists)
            return Result.Failure(StaffErrors.PositionNotFound);

        collaborator.AddAssignment(
            request.PositionId,
            AssignmentType.Position,
            isPrimary: false
        );

        await context.SaveChangesAsync();

        return Result.Success();
    }
}