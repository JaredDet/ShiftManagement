using ShiftManagement.Api.BuildingBlocks.Results;
using ShiftManagement.Api.Modules.Contracts.Domain;
using ShiftManagement.Api.Modules.Contracts.Infrastructure;
using ShiftManagement.Api.Infrastructure.Persistence;
using ShiftManagement.Api.Modules.Identity.Infrastructure;
using ShiftManagement.Api.BuildingBlocks.Execution;
using ShiftManagement.Api.Modules.Contracts.Api.Contracts.Managements;
using ShiftManagement.Api.Modules.Identity.Application;

namespace ShiftManagement.Api.Modules.Contracts.Application.Command;

public sealed class TerminateContractUseCase(
    ContractRepository contractRepository,
    UserRepository userRepository,
    IExecutionContext context,
    ShiftManagementDbContext dbContext
)
{
    public async Task<Result> ExecuteAsync(
        Guid contractId,
        TerminateEmploymentContractRequest request)
    {
        var userExists = await userRepository.ExistsAsync(context.UserId);

        if (!userExists)
        {
            return Result.Failure(
                IdentityErrors.UserNotFound);
        }

        var contract = await contractRepository.GetByIdAsync(contractId);

        if (contract is null)
        {
            return Result.Failure(
                ContractErrors.ContractNotFound);
        }

        contract.Terminate(
            request.TerminationDate,
            request.Reason,
            request.Comment,
            context.UserId,
            null // TODO: settlementDocumentUrl 
        );

        await dbContext.SaveChangesAsync();

        return Result.Success();
    }
}