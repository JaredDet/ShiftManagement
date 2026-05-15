using ShiftManagement.Api.Infrastructure;
using ShiftManagement.Api.Modules.Scheduling.Api.Contracts;
using ShiftManagement.Api.Modules.Scheduling.Domain;
using ShiftManagement.Api.Modules.Scheduling.Infrastructure;
using ShiftManagement.Api.Shared;

namespace ShiftManagement.Api.Modules.Scheduling.Application;

public sealed class AssignCollaboratorToShiftUseCase(
    ShiftRepository shiftRepository,
    ShiftAssignmentRepository assignmentRepository,
    ShiftManagementDbContext context
)
{
    public async Task<Result<ShiftAssignmentResponse>> ExecuteAsync(
        AssignCollaboratorToShiftRequest request
    )
    {
        var shift = await shiftRepository.GetByIdAsync(request.ShiftId);

        if (shift is null)
            return Result<ShiftAssignmentResponse>.Failure(SchedulingErrors.ShiftNotFound);

        if (shift.Status != ShiftStatus.Active)
            return Result<ShiftAssignmentResponse>.Failure(SchedulingErrors.InvalidShiftState);

        var alreadyAssigned = await assignmentRepository
            .ExistsActiveAssignmentAsync(request.ShiftId, request.CollaboratorId);

        if (alreadyAssigned)
            return Result<ShiftAssignmentResponse>.Failure(SchedulingErrors.ShiftAlreadyAssigned);

        var assignment = ShiftAssignment.Create(
            request.ShiftId,
            request.CollaboratorId
        );

        await assignmentRepository.AddAsync(assignment);
        await context.SaveChangesAsync();

        return Result<ShiftAssignmentResponse>.Success(
            ShiftAssignmentMapper.ToResponse(
                assignment
            )
        );
    }
}