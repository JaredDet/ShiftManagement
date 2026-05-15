namespace ShiftManagement.Api.Modules.Scheduling.Api.Contracts;

public sealed record CancelShiftSwapRequest
{
    public Guid SwapRequestId { get; init; }
}