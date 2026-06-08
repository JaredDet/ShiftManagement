using ShiftManagement.Api.Modules.Staff.Api.Contracts.MainChanges;
using ShiftManagement.Api.Modules.Staff.Infrastructure;

using ShiftManagement.Api.Modules.Staff.Application.Errors;
using ShiftManagement.Api.Modules.Staff.Domain;
using ShiftManagement.Api.Infrastructure.Persistence;
using ShiftManagement.Api.BuildingBlocks.Results;
using ShiftManagement.Api.Modules.Staff.Api.Contracts.Collaborators;

namespace ShiftManagement.Api.Modules.Staff.Application.EmploymentAssignments;

public sealed class ChangeMainBranchUseCase(
    CollaboratorRepository CollaboratorRepository,
    CollaboratorReadRepository collaboratorReadRepository,
    ShiftManagementDbContext context
)
{
    public async Task<Result<CollaboratorResponse>> Execute(
        Guid collaboratorId,
        ChangeMainBranchRequest request
    )
    {
        var collaborator = await CollaboratorRepository.GetByIdAsync(collaboratorId);

        if (collaborator is null)
            return Result<CollaboratorResponse>.Failure(StaffErrors.CollaboratorNotFound);

        collaborator.SetPrimary(
           request.BranchId,
           AssignmentType.Branch
       );

        await context.SaveChangesAsync();

        var result = await collaboratorReadRepository.GetByIdAsync(collaborator.Id, collaborator.CompanyId);

        if (result is null)
            return Result<CollaboratorResponse>.Failure(StaffErrors.CollaboratorNotFound);

        return Result<CollaboratorResponse>.Success(result);
    }
}