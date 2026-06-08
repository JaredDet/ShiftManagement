using ShiftManagement.Api.Modules.Staff.Application.Errors;
using ShiftManagement.Api.Modules.Staff.Infrastructure;
using ShiftManagement.Api.Infrastructure.Persistence;
using ShiftManagement.Api.BuildingBlocks.Results;

namespace ShiftManagement.Api.Modules.Staff.Application.Collaborators;

public sealed class DeactivateCollaboratorUseCase(
    CollaboratorRepository repository,
    ShiftManagementDbContext context
)
{
    public async Task<Result> Execute(Guid companyId, Guid id)
    {
        var collaborator = await repository.GetByIdAsync(companyId, id);

        if (collaborator is null)
            return Result.Failure(StaffErrors.CollaboratorNotFound);

        collaborator.Deactivate();

        await context.SaveChangesAsync();

        return Result.Success();
    }
}