namespace ShiftManagement.Api.Modules.Scheduling.Api.Contracts;

public sealed record RejectShiftSwapRequest
{
    public Guid SwapRequestId { get; init; }
}