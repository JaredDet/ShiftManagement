using ShiftManagement.Api.Modules.Scheduling.Api.Contracts;
using ShiftManagement.Api.Modules.Scheduling.Domain;

namespace ShiftManagement.Api.Modules.Scheduling.Application.Swaps;

public static class ShiftSwapMapper
{
    public static ShiftSwapRequestResponse ToResponse(ShiftSwapRequest swap)
    {
        return new ShiftSwapRequestResponse
        {
            Id = swap.Id,
            RequesterId = swap.RequesterId,
            TargetCollaboratorId = swap.TargetCollaboratorId,
            SourceShiftId = swap.SourceShiftId,
            TargetShiftId = swap.TargetShiftId,
            Status = swap.Status.ToString(),
            CreatedAt = swap.CreatedAt,
            RespondedAt = swap.RespondedAt,
            ApprovedAt = swap.ApprovedAt
        };
    }
}