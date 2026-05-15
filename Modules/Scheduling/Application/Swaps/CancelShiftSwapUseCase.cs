using ShiftManagement.Api.Infrastructure;
using ShiftManagement.Api.Modules.Scheduling.Api.Contracts;
using ShiftManagement.Api.Modules.Scheduling.Infrastructure;
using ShiftManagement.Api.Shared;

namespace ShiftManagement.Api.Modules.Scheduling.Application.Swaps;

public sealed class CancelShiftSwapUseCase(
    ShiftSwapRequestRepository swapRepository,
    ShiftManagementDbContext context
)
{
    public async Task<Result<ShiftSwapRequestResponse>> ExecuteAsync(Guid swapId)
    {
        try
        {
            var swap = await swapRepository.GetByIdAsync(swapId);

            if (swap is null)
                return Result<ShiftSwapRequestResponse>.Failure(
                    SchedulingErrors.SwapRequestNotFound
                );

            swap.Cancel();

            await context.SaveChangesAsync();

            return Result<ShiftSwapRequestResponse>.Success(
                ShiftSwapMapper.ToResponse(swap)
            );
        }
        catch (DomainException ex)
        {
            return Result<ShiftSwapRequestResponse>.Failure(
                new Error(ex.Code, ex.Message)
            );
        }
    }
}