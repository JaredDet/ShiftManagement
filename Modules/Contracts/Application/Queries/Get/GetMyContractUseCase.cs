using ShiftManagement.Api.BuildingBlocks.Results;
using ShiftManagement.Api.Modules.Contracts.Infrastructure;
using ShiftManagement.Api.BuildingBlocks.Execution;
using ShiftManagement.Api.Modules.Contracts.Api.Contracts.Query;

namespace ShiftManagement.Api.Modules.Contracts.Application.Queries;

public sealed class GetMyContractUseCase(
    ContractReadRepository contractReadRepository,
    IExecutionContext context
)
{
    public async Task<Result<MyEmploymentContractResponse>> ExecuteAsync()
    {
        var contract = await contractReadRepository
            .GetMyContractAsync(context.UserId);

        if (contract is null)
        {
            return Result<MyEmploymentContractResponse>.Failure(
                ContractErrors.ContractNotFound);
        }

        return Result<MyEmploymentContractResponse>.Success(contract);
    }
}