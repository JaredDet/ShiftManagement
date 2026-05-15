using ShiftManagement.Api.Shared;

namespace ShiftManagement.Api.Modules.Scheduling.Api.Contracts;

public sealed record ApproveShiftSwapRequest
{
    [NotEmptyGuid]
    public Guid SwapRequestId { get; init; }
}