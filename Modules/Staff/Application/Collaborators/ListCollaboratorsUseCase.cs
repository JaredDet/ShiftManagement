using ShiftManagement.Api.Modules.Staff.Api.Contracts.Collaborators;
using ShiftManagement.Api.Modules.Staff.Infrastructure;



using ShiftManagement.Api.BuildingBlocks.Results;

namespace ShiftManagement.Api.Modules.Staff.Application.Collaborators;

public sealed class ListCollaboratorsUseCase(
    CollaboratorReadRepository repository
)
{
    public async Task<Result<List<CollaboratorResponse>>> Execute(
    Guid companyId
)
    {
        var collaborators = await repository.ListAsync(companyId);

        return Result<List<CollaboratorResponse>>
            .Success(collaborators);
    }
}