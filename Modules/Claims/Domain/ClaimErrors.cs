using ShiftManagement.Api.Shared;

namespace ShiftManagement.Api.Modules.Claims.Domain;

public static class ClaimStateErrors
{
    public static DomainException InvalidStateTransition(
        ClaimStatus current,
        ClaimAction action)
    {
        return new DomainException(
            "claims.invalid_transition",
            $"Cannot execute '{action}' from '{current}'"
        );
    }

    public static DomainException ClaimAlreadyClosed(Guid claimId)
    {
        return new DomainException(
            "claims.already_closed",
            $"Claim '{claimId}' is already closed"
        );
    }
}