using ShiftManagement.Api.Shared;
using ShiftManagement.Api.Modules.Staff.Api.Contracts.Collaborators;
using ShiftManagement.Api.Modules.Staff.Infrastructure.Persistence.Repositories;

namespace ShiftManagement.Api.Modules.Staff.Application.Collaborators;

public sealed class ListCollaboratorsUseCase(
    CollaboratorReadRepository repository
)
{
    public async Task<Result<CollaboratorListResponse>> Execute(Guid companyId)
    {
        var collaborators = await repository.ListAsync(companyId);

        return Result<CollaboratorListResponse>.Success(new CollaboratorListResponse
        {
            Collaborators = collaborators
        });
    }
}