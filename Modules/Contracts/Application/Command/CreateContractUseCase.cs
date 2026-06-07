using ShiftManagement.Api.BuildingBlocks.Results;
using ShiftManagement.Api.Modules.Contracts.Domain;
using ShiftManagement.Api.Modules.Contracts.Infrastructure;
using ShiftManagement.Api.Modules.Staff.Infrastructure;
using ShiftManagement.Api.Modules.Staff.Application.Errors;
using ShiftManagement.Api.Infrastructure.Persistence;
using ShiftManagement.Api.Modules.Identity.Infrastructure;
using ShiftManagement.Api.Modules.Identity.Application;
using ShiftManagement.Api.Modules.Contracts.Api.Contracts.Managements;
using ShiftManagement.Api.BuildingBlocks.Execution;

namespace ShiftManagement.Api.Modules.Contracts.Application.Command;

public sealed class CreateContractUseCase(
    ContractRepository contractRepository,
    CollaboratorRepository collaboratorRepository,
    UserRepository userRepository,
    IExecutionContext context,
    ShiftManagementDbContext dbContext
)
{
    public async Task<Result<Guid>> ExecuteAsync(
        CreateEmploymentContractRequest request)
    {
        var collaboratorExists =
            await collaboratorRepository.ExistsAsync(request.CollaboratorId);

        if (!collaboratorExists)
        {
            return Result<Guid>.Failure(
                StaffErrors.CollaboratorNotFound);
        }

        var creatorExists =
            await userRepository.ExistsAsync(context.UserId);

        if (!creatorExists)
        {
            return Result<Guid>.Failure(
                IdentityErrors.UserNotFound);
        }

        var contract = EmploymentContract.Create(
            request.CollaboratorId,
            context.UserId,
            request.Type,
            request.WorkScheduleType,
            request.SalaryAmount,
            request.Currency,
            request.PayPeriod,
            request.StartsAt,
            request.EndsAt
        );

        await contractRepository.AddAsync(contract);

        await dbContext.SaveChangesAsync();

        return Result<Guid>.Success(contract.Id);
    }
}