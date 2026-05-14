using ShiftManagement.Api.Shared;
using ShiftManagement.Api.Modules.Staff.Infrastructure.Persistence.Repositories;
using ShiftManagement.Api.Modules.Staff.Application.Errors;
using ShiftManagement.Api.Modules.Staff.Domain;
using ShiftManagement.Api.Modules.Organization.Infrastructure.Persistence.Repositories;
using ShiftManagement.Api.Infrastructure;
using ShiftManagement.Api.Modules.Organization.Application.Errors;
using ShiftManagement.Api.Modules.Staff.Api.Contracts.BranchAssignments;

namespace ShiftManagement.Api.Modules.Staff.Application.EmploymentAssignments;

public sealed class AssignBranchToCollaboratorUseCase(
    EmployeeRepository employeeRepository,
    BranchRepository branchRepository,
    ShiftManagementDbContext context
)
{
    public async Task<Result> Execute(Guid employeeId, AssignBranchToCollaboratorRequest request)
    {
        var employee = await employeeRepository.GetByIdAsync(employeeId);

        if (employee is null)
            return Result.Failure(StaffErrors.EmployeeNotFound);

        var exists = await branchRepository.ExistsAsync(request.BranchId);

        if (!exists)
            return Result.Failure(OrganizationErrors.BranchNotFound);

        var result = employee.AddAssignment(
            request.BranchId,
            AssignmentType.Branch,
            isPrimary: false
        );

        if (!result.IsSuccess)
            return result;

        await context.SaveChangesAsync();

        return Result.Success();
    }
}