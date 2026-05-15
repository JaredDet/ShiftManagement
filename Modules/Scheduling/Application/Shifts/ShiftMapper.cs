using ShiftManagement.Api.Modules.Scheduling.Api.Contracts;
using ShiftManagement.Api.Modules.Scheduling.Domain;

namespace ShiftManagement.Api.Modules.Scheduling.Application.Shifts;

public static class ShiftMapper
{
    public static ShiftResponse ToResponse(
        Shift shift,
        string branchName,
        string positionName
    )
    {
        return new ShiftResponse
        {
            Id = shift.Id,
            BranchId = shift.BranchId,
            BranchName = branchName,
            PositionId = shift.PositionId,
            PositionName = positionName,
            StartsAt = shift.StartsAt,
            EndsAt = shift.EndsAt,
            Status = shift.Status.ToString(),
            CreatedAt = shift.CreatedAt
        };
    }
}