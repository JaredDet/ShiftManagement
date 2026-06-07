using ShiftManagement.Api.Modules.Staff.Application.Errors;
using ShiftManagement.Api.Modules.Staff.Domain;
using ShiftManagement.Api.Modules.Organization.Infrastructure.Persistence.Repositories;
using ShiftManagement.Api.Modules.Organization.Application.Errors;
using ShiftManagement.Api.Modules.Staff.Api.Contracts.BranchAssignments;
using ShiftManagement.Api.Infrastructure.Persistence;
using ShiftManagement.Api.BuildingBlocks.Results;
using ShiftManagement.Api.Modules.Staff.Infrastructure;

namespace ShiftManagement.Api.Modules.Staff.Application.EmploymentAssignments;

public sealed class AssignBranchToCollaboratorUseCase(
    CollaboratorRepository CollaboratorRepository,
    BranchRepository branchRepository,
    ShiftManagementDbContext context
)
{
    public async Task<Result> Execute(Guid collaboratorId, AssignBranchToCollaboratorRequest request)
    {
        var collaborator = await CollaboratorRepository.GetByIdAsync(collaboratorId);

        if (collaborator is null)
            return Result.Failure(StaffErrors.CollaboratorNotFound);

        var exists = await branchRepository.ExistsAsync(request.BranchId);

        if (!exists)
            return Result.Failure(OrganizationErrors.BranchNotFound);

        collaborator.AddAssignment(
            request.BranchId,
            AssignmentType.Branch,
            isPrimary: false
        );

        await context.SaveChangesAsync();

        return Result.Success();
    }
}