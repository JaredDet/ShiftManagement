using ShiftManagement.Api.BuildingBlocks.Results;
using ShiftManagement.Api.Modules.Contracts.Api.Contracts.Queries;
using ShiftManagement.Api.Modules.Contracts.Infrastructure;

namespace ShiftManagement.Api.Modules.Contracts.Application.Queries;

public sealed class GetActiveContractUseCase(
    ContractReadRepository contractReadRepository
)
{
    public async Task<Result<EmploymentContractResponse>> ExecuteAsync(
        Guid collaboratorId)
    {
        var contract = await contractReadRepository
            .GetActiveByCollaboratorAsync(collaboratorId);

        if (contract is null)
        {
            return Result<EmploymentContractResponse>.Failure(
                ContractErrors.ActiveContractNotFound(collaboratorId));
        }

        return Result<EmploymentContractResponse>.Success(contract);
    }
}