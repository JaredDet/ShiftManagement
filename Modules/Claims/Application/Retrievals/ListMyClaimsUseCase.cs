using ShiftManagement.Api.BuildingBlocks.Execution;
using ShiftManagement.Api.BuildingBlocks.Results;
using ShiftManagement.Api.Modules.Claims.Api.Contracts.Responses;
using ShiftManagement.Api.Modules.Claims.Infrastructure;
using ShiftManagement.Api.Modules.Staff.Application.Errors;
using ShiftManagement.Api.Modules.Staff.Infrastructure.Persistence.Repositories;

namespace ShiftManagement.Api.Modules.Claims.Application.Retrievals;

public sealed class ListMyClaimsUseCase(
    ClaimReadRepository repository,
    CollaboratorRepository collaboratorRepository,
    IExecutionContext context
)
{
    public async Task<Result<List<MyClaimResponse>>> ExecuteAsync()
    {
        var collaborator = await collaboratorRepository
            .GetByUserAndCompanyAsync(
                context.UserId,
                context.CompanyId
            );

        if (collaborator is null)
        {
            return Result<List<MyClaimResponse>>.Failure(
                StaffErrors.CollaboratorNotFound
            );
        }

        var claims = await repository.ListByCollaboratorAsync(
            context.CompanyId,
            collaborator.Id
        );

        return Result<List<MyClaimResponse>>.Success(
            claims
        );
    }
}