using ShiftManagement.Api.BuildingBlocks.Results;
using ShiftManagement.Api.Modules.Contracts.Api.Contracts.Queries;
using ShiftManagement.Api.Modules.Contracts.Infrastructure;

namespace ShiftManagement.Api.Modules.Contracts.Application.Queries;

public sealed class ListCollaboratorContractsUseCase(
    ContractReadRepository contractReadRepository
)
{
    public async Task<Result<List<EmploymentContractResponse>>> ExecuteAsync(
        Guid collaboratorId)
    {
        var contracts = await contractReadRepository
            .ListByCollaboratorAsync(collaboratorId);

        return Result<List<EmploymentContractResponse>>.Success(contracts);
    }
}