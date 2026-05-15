using ShiftManagement.Api.Infrastructure;
using ShiftManagement.Api.Modules.Scheduling.Api.Contracts;
using ShiftManagement.Api.Modules.Scheduling.Domain;
using ShiftManagement.Api.Modules.Scheduling.Infrastructure;
using ShiftManagement.Api.Shared;

namespace ShiftManagement.Api.Modules.Scheduling.Application.Shifts;

public sealed class ReplaceCollaboratorInShiftUseCase(
    ShiftRepository shiftRepository,
    ShiftAssignmentRepository assignmentRepository,
    ShiftManagementDbContext context
)
{
    public async Task<Result<ShiftAssignmentResponse>> ExecuteAsync(
        ReplaceCollaboratorInShiftRequest request
    )
    {
        var shift = await shiftRepository.GetByIdAsync(request.ShiftId);

        if (shift is null)
            return Result<ShiftAssignmentResponse>.Failure(SchedulingErrors.ShiftNotFound);

        if (shift.Status != ShiftStatus.Active)
            return Result<ShiftAssignmentResponse>.Failure(SchedulingErrors.InvalidShiftState);

        var currentAssignment = await assignmentRepository
            .GetActiveAssignmentAsync(request.ShiftId, request.CurrentCollaboratorId);

        if (currentAssignment is null)
            return Result<ShiftAssignmentResponse>.Failure(SchedulingErrors.AssignmentNotFound);

        var alreadyAssigned = await assignmentRepository
            .ExistsActiveAssignmentAsync(request.ShiftId, request.NewCollaboratorId);

        if (alreadyAssigned)
            return Result<ShiftAssignmentResponse>.Failure(SchedulingErrors.ShiftAlreadyAssigned);

        currentAssignment.Cancel();

        var newAssignment = ShiftAssignment.Create(
            request.ShiftId,
            request.NewCollaboratorId
        );

        await assignmentRepository.AddAsync(newAssignment);

        await context.SaveChangesAsync();

        return Result<ShiftAssignmentResponse>.Success(
            ShiftAssignmentMapper.ToResponse(newAssignment)
        );
    }
}