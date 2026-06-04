using ShiftManagement.Api.BuildingBlocks.Exceptions;

namespace ShiftManagement.Api.Modules.Session.Application;

public static class SessionErrors
{
    public static readonly Error SessionNotFound =
        new(
            "session.not_found",
            "Session not found.",
            ErrorType.NotFound
        );

    public static readonly Error BranchAccessDenied =
        new(
            "session.branch.access_denied",
            "No access to this branch.",
            ErrorType.Forbidden
        );
}