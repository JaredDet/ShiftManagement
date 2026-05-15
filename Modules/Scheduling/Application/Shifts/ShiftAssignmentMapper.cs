using ShiftManagement.Api.Modules.Scheduling.Api.Contracts;
using ShiftManagement.Api.Modules.Scheduling.Domain;

namespace ShiftManagement.Api.Modules.Scheduling.Application.Shifts;

public static class ShiftAssignmentMapper
{
    public static ShiftAssignmentResponse ToResponse(ShiftAssignment assignment)
    {
        return new ShiftAssignmentResponse
        {
            Id = assignment.Id,
            ShiftId = assignment.ShiftId,
            CollaboratorId = assignment.CollaboratorId,
            Status = assignment.Status.ToString(),
            CreatedAt = assignment.CreatedAt,
            CancelledAt = assignment.CancelledAt
        };
    }
}