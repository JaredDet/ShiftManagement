using ShiftManagement.Api.Modules.Staff.Api.Contracts.PositionAssignments;
using ShiftManagement.Api.Modules.Staff.Infrastructure.Persistence.Repositories;
using ShiftManagement.Api.Modules.Staff.Application.Errors;
using ShiftManagement.Api.Modules.Staff.Domain;
using ShiftManagement.Api.Infrastructure.Persistence;
using ShiftManagement.Api.BuildingBlocks.Results;

namespace ShiftManagement.Api.Modules.Staff.Application.EmploymentAssignments;

public sealed class AssignPositionToCollaboratorUseCase(
    EmployeeRepository employeeRepository,
    PositionRepository positionRepository,
    ShiftManagementDbContext context
)
{
    public async Task<Result> Execute(Guid employeeId, AssignPositionToCollaboratorRequest request)
    {
        var employee = await employeeRepository.GetByIdAsync(employeeId);

        if (employee is null)
            return Result.Failure(StaffErrors.EmployeeNotFound);

        var exists = await positionRepository.ExistsAsync(request.PositionId);

        if (!exists)
            return Result.Failure(StaffErrors.PositionNotFound);

        employee.AddAssignment(
            request.PositionId,
            AssignmentType.Position,
            isPrimary: false
        );

        await context.SaveChangesAsync();

        return Result.Success();
    }
}