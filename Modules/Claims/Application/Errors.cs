using ShiftManagement.Api.BuildingBlocks.Exceptions;

namespace ShiftManagement.Api.Modules.Claims.Application;

public static class ClaimErrors
{
    public static readonly Error ClaimNotFound =
        new(
            "claims.claim.not_found",
            "Claim not found",
            ErrorType.NotFound
        );

    public static readonly Error ClaimAlreadyClosed =
        new(
            "claims.claim.already_closed",
            "Claim is already closed",
            ErrorType.Conflict
        );

    public static readonly Error InvalidClaimState =
        new(
            "claims.claim.invalid_state",
            "Claim is in an invalid state for this operation",
            ErrorType.Conflict
        );

    public static readonly Error ClaimAlreadyAssigned =
        new(
            "claims.claim.already_assigned",
            "Claim is already assigned",
            ErrorType.Conflict
        );

    public static readonly Error CommentNotFound =
        new(
            "claims.comment.not_found",
            "Comment not found",
            ErrorType.NotFound
        );

    public static readonly Error EvidenceNotFound =
        new(
            "claims.evidence.not_found",
            "Evidence not found",
            ErrorType.NotFound
        );

    public static readonly Error UnauthorizedClaimAccess =
        new(
            "claims.claim.unauthorized_access",
            "User is not authorized to access this claim",
            ErrorType.Forbidden
        );

    public static readonly Error InvalidClaimPriority =
        new(
            "claims.claim.invalid_priority",
            "Invalid claim priority",
            ErrorType.Validation
        );

    public static readonly Error InvalidClaimReason =
        new(
            "claims.claim.invalid_reason",
            "Invalid claim reason",
            ErrorType.Validation
        );
}