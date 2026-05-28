using ShiftManagement.Api.Modules.Staff.Domain;
using ShiftManagement.Api.Modules.Staff.Infrastructure.Persistence.Repositories;
using ShiftManagement.Api.Modules.Staff.Application.Errors;
using ShiftManagement.Api.Modules.Staff.Api.Contracts.BranchAssignments;
using ShiftManagement.Api.Infrastructure.Persistence;
using ShiftManagement.Api.BuildingBlocks.Results;

namespace ShiftManagement.Api.Modules.Staff.Application.EmploymentAssignments;

public sealed class RemoveBranchFromCollaboratorUseCase(
    CollaboratorRepository CollaboratorRepository,
    ShiftManagementDbContext context
)
{
    public async Task<Result> Execute(
        Guid collaboratorId,
        RemoveBranchFromCollaboratorRequest request
    )
    {
        var collaborator = await CollaboratorRepository.GetByIdAsync(collaboratorId);

        if (collaborator is null)
            return Result.Failure(StaffErrors.CollaboratorNotFound);

        collaborator.RemoveAssignment(
            request.BranchId,
            AssignmentType.Branch
        );

        await context.SaveChangesAsync();

        return Result.Success();
    }
}