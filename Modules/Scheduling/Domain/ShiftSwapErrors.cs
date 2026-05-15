using ShiftManagement.Api.Shared;

namespace ShiftManagement.Api.Modules.Scheduling.Domain;

public static class ShiftSwapErrors
{
    public static DomainException InvalidState(string operation, ShiftSwapStatus status) =>
    new(
        "scheduling.shift.invalid_state",
        $"Cannot perform '{operation}' when swap is in state '{status}'"
    );
}