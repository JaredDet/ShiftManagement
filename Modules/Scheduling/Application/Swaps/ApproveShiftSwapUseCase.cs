using ShiftManagement.Api.BuildingBlocks.Results;
using ShiftManagement.Api.Infrastructure.Persistence;
using ShiftManagement.Api.Modules.Scheduling.Api.Contracts;
using ShiftManagement.Api.Modules.Scheduling.Infrastructure;

namespace ShiftManagement.Api.Modules.Scheduling.Application.Swaps;

public sealed class ApproveShiftSwapUseCase(
    ShiftSwapRepository swapRepository,
    ShiftAssignmentRepository assignmentRepository,
    ShiftSwapReadRepository swapReadRepository,
    ShiftManagementDbContext context
)
{
    public async Task<Result<ShiftSwapRequestResponse>> ExecuteAsync(Guid swapId)
    {
        var swap = await swapRepository.GetByIdAsync(swapId);

        if (swap is null)
            return Result<ShiftSwapRequestResponse>.Failure(
                SchedulingErrors.SwapRequestNotFound
            );

        swap.Approve();

        var sourceAssignment = await assignmentRepository
            .GetActiveByShiftIdAsync(swap.SourceShiftId);

        var targetAssignment = await assignmentRepository
            .GetActiveByShiftIdAsync(swap.TargetShiftId);

        if (sourceAssignment is null || targetAssignment is null)
            return Result<ShiftSwapRequestResponse>.Failure(
                SchedulingErrors.AssignmentNotFound
            );

        var sourceCollaboratorId = sourceAssignment.CollaboratorId;
        var targetCollaboratorId = targetAssignment.CollaboratorId;

        sourceAssignment.TransferTo(targetCollaboratorId);
        targetAssignment.TransferTo(sourceCollaboratorId);

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