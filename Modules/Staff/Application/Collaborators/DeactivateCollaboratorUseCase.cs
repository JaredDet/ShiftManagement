using ShiftManagement.Api.Modules.Staff.Application.Errors;
using ShiftManagement.Api.Modules.Staff.Infrastructure.Persistence.Repositories;
using ShiftManagement.Api.Infrastructure;
using ShiftManagement.Api.Shared;

namespace ShiftManagement.Api.Modules.Staff.Application.Collaborators;

public sealed class DeactivateCollaboratorUseCase(
    EmployeeRepository repository,
    ShiftManagementDbContext context
)
{
    public async Task<Result> Execute(Guid id)
    {
        var employee = await repository.GetByIdAsync(id);

        if (employee is null)
            return Result.Failure(StaffErrors.EmployeeNotFound);

        employee.Deactivate();

        await context.SaveChangesAsync();

        return Result.Success();
    }
}