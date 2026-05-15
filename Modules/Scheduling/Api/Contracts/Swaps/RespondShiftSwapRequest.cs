using ShiftManagement.Api.Modules.Scheduling.Application.Swaps;
using ShiftManagement.Api.Shared;

namespace ShiftManagement.Api.Modules.Scheduling.Api.Contracts;

public sealed record RespondShiftSwapRequest
{

    [NotEmptyGuid]
    public Guid SwapRequestId { get; init; }
    public ShiftSwapDecision Decision { get; init; }
}