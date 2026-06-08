using ShiftManagement.Api.BuildingBlocks.Results;
using ShiftManagement.Api.Infrastructure.Persistence;
using ShiftManagement.Api.Modules.Scheduling.Api.Contracts;
using ShiftManagement.Api.Modules.Scheduling.Infrastructure;

namespace ShiftManagement.Api.Modules.Scheduling.Application.Swaps;

public sealed class RespondToShiftSwapUseCase(
    ShiftSwapRepository swapRepository,
    ShiftSwapReadRepository swapReadRepository,
    ShiftManagementDbContext context
)
{
    public async Task<Result<ShiftSwapRequestResponse>> ExecuteAsync(
        Guid swapId,
        ShiftSwapDecision decision
    )
    {
        var swap = await swapRepository.GetByIdAsync(swapId);

        if (swap is null)
            return Result<ShiftSwapRequestResponse>.Failure(
                SchedulingErrors.SwapRequestNotFound
            );

        switch (decision)
        {
            case ShiftSwapDecision.Accept:
                swap.Accept();
                break;

            case ShiftSwapDecision.Reject:
                swap.Reject();
                break;

            default:
                return Result<ShiftSwapRequestResponse>.Failure(
                    SchedulingErrors.InvalidSwapDecision
                );
        }

        await context.SaveChangesAsync();

        var response = await swapReadRepository.GetByIdAsync(swapId);

        if (response == null)
        {
            return Result<ShiftSwapRequestResponse>.Failure(SchedulingErrors.SwapRequestNotFound);
        }

        return Result<ShiftSwapRequestResponse>.Success(
            response
        );
    }
}