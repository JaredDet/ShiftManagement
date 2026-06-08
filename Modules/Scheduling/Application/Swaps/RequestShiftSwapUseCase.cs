using ShiftManagement.Api.BuildingBlocks.Results;
using ShiftManagement.Api.Infrastructure.Persistence;
using ShiftManagement.Api.Modules.Scheduling.Api.Contracts;
using ShiftManagement.Api.Modules.Scheduling.Domain;
using ShiftManagement.Api.Modules.Scheduling.Infrastructure;

namespace ShiftManagement.Api.Modules.Scheduling.Application.Swaps;

public sealed class RequestShiftSwapUseCase(
    ShiftRepository shiftRepository,
    ShiftAssignmentRepository assignmentRepository,
    ShiftSwapRepository swapRepository,
    ShiftSwapReadRepository swapReadRepository,
    ShiftManagementDbContext context
)
{
    public async Task<Result<ShiftSwapRequestResponse>> ExecuteAsync(
        RequestShiftSwapRequest request
    )
    {
        var sourceShift = await shiftRepository.GetByIdAsync(request.SourceShiftId);
        if (sourceShift is null)
            return Result<ShiftSwapRequestResponse>.Failure(SchedulingErrors.ShiftNotFound);

        var targetShift = await shiftRepository.GetByIdAsync(request.TargetShiftId);
        if (targetShift is null)
            return Result<ShiftSwapRequestResponse>.Failure(SchedulingErrors.ShiftNotFound);

        sourceShift.EnsureCanParticipateInSwap();
        targetShift.EnsureCanParticipateInSwap();

        var sourceAssignment = await assignmentRepository
            .GetActiveByShiftIdAsync(request.SourceShiftId);

        var targetAssignment = await assignmentRepository
            .GetActiveByShiftIdAsync(request.TargetShiftId);

        if (sourceAssignment is null || targetAssignment is null)
            return Result<ShiftSwapRequestResponse>.Failure(SchedulingErrors.AssignmentNotFound);

        var swapRequest = ShiftSwapRequest.Create(
            sourceAssignment.CollaboratorId,
            targetAssignment.CollaboratorId,
            sourceShift.Id,
            targetShift.Id
        );

        await swapRepository.AddAsync(swapRequest);
        await context.SaveChangesAsync();

        var response = await swapReadRepository.GetByIdAsync(swapRequest.Id);

        if (response == null)
        {
            return Result<ShiftSwapRequestResponse>.Failure(SchedulingErrors.SwapRequestNotFound);
        }

        return Result<ShiftSwapRequestResponse>.Success(
            response
        );
    }
}