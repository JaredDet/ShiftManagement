using ShiftManagement.Api.Modules.Staff.Api.Contracts.MainChanges;
using ShiftManagement.Api.Modules.Staff.Infrastructure;



using ShiftManagement.Api.Modules.Staff.Application.Errors;
using ShiftManagement.Api.Modules.Staff.Application.Mappers;
using ShiftManagement.Api.Modules.Staff.Domain;
using ShiftManagement.Api.Modules.Staff.Api.Contracts.Collaborators;
using ShiftManagement.Api.Infrastructure.Persistence;
using ShiftManagement.Api.BuildingBlocks.Results;

namespace ShiftManagement.Api.Modules.Staff.Application.EmploymentAssignments;

public sealed class ChangeMainPositionUseCase(
    CollaboratorRepository CollaboratorRepository,
    ShiftManagementDbContext context
)
{
    public async Task<Result<CollaboratorResponse>> Execute(
        Guid collaboratorId,
        ChangeMainPositionRequest request
    )
    {
        var collaborator = await CollaboratorRepository.GetByIdAsync(collaboratorId);

        if (collaborator is null)
            return Result<CollaboratorResponse>.Failure(StaffErrors.CollaboratorNotFound);

        collaborator.SetPrimary(
            request.PositionId,
            AssignmentType.Position
        );

        await context.SaveChangesAsync();

        return Result<CollaboratorResponse>.Success(
            CollaboratorMapper.ToResponse(collaborator)
        );
    }
}