using ShiftManagement.Api.BuildingBlocks.Results;
using ShiftManagement.Api.Modules.Contracts.Api.Contracts.Queries;
using ShiftManagement.Api.Modules.Contracts.Infrastructure;

namespace ShiftManagement.Api.Modules.Contracts.Application.Queries;

public sealed class GetContractUseCase(
    ContractReadRepository contractReadRepository
)
{
    public async Task<Result<EmploymentContractResponse>> ExecuteAsync(
        Guid contractId)
    {
        var contract = await contractReadRepository.GetByIdAsync(contractId);

        if (contract is null)
        {
            return Result<EmploymentContractResponse>.Failure(
                ContractErrors.ContractNotFound);
        }

        return Result<EmploymentContractResponse>.Success(contract);
    }
}