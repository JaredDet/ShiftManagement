using ShiftManagement.Api.Modules.Staff.Domain;
using ShiftManagement.Api.Modules.Staff.Infrastructure.Persistence.Repositories;
using ShiftManagement.Api.Modules.Staff.Application.Errors;
using ShiftManagement.Api.Modules.Staff.Api.Contracts.BranchAssignments;
using ShiftManagement.Api.Infrastructure.Persistence;
using ShiftManagement.Api.BuildingBlocks.Results;

namespace ShiftManagement.Api.Modules.Staff.Application.EmploymentAssignments;

public sealed class RemoveBranchFromCollaboratorUseCase(
    EmployeeRepository employeeRepository,
    ShiftManagementDbContext context
)
{
    public async Task<Result> Execute(
        Guid employeeId,
        RemoveBranchFromCollaboratorRequest request
    )
    {
        var employee = await employeeRepository.GetByIdAsync(employeeId);

        if (employee is null)
            return Result.Failure(StaffErrors.EmployeeNotFound);

        employee.RemoveAssignment(
            request.BranchId,
            AssignmentType.Branch
        );

        await context.SaveChangesAsync();

        return Result.Success();
    }
}