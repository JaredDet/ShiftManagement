using ShiftManagement.Api.Modules.Staff.Api.Contracts.Collaborators;
using ShiftManagement.Api.Modules.Staff.Infrastructure.Persistence.Repositories;
using ShiftManagement.Api.Modules.Staff.Application.Errors;
using ShiftManagement.Api.BuildingBlocks.Results;

namespace ShiftManagement.Api.Modules.Staff.Application.Collaborators;

public sealed class GetCollaboratorUseCase(
    CollaboratorReadRepository repository
)
{
    public async Task<Result<CollaboratorResponse>> Execute(Guid id, Guid companyId)
    {
        var collaborator = await repository.GetByIdAsync(id, companyId);

        if (collaborator is null)
            return Result<CollaboratorResponse>.Failure(StaffErrors.EmployeeNotFound);

        return Result<CollaboratorResponse>.Success(collaborator);
    }
}