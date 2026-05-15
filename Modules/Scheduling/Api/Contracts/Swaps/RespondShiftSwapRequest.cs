using ShiftManagement.Api.Modules.Scheduling.Application.Swaps;

namespace ShiftManagement.Api.Modules.Scheduling.Api.Contracts;

public sealed record RespondShiftSwapRequest
{
    public Guid SwapRequestId { get; init; }
    public ShiftSwapDecision Decision { get; init; }
}