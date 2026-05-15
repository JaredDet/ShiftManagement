
using ShiftManagement.Api.Shared;

namespace ShiftManagement.Api.Modules.Scheduling.Domain;

public static class ShiftErrors
{
    public static DomainException InvalidStateForSwap() =>
    new("scheduling.shift.invalid_state_for_swap", "Shift cannot participate in a swap in its current state");
}