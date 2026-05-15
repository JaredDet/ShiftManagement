namespace ShiftManagement.Api.Modules.Scheduling.Api.Contracts;

public sealed record ApproveShiftSwapRequest
{
    public Guid SwapRequestId { get; init; }
}