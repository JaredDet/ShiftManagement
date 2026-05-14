using ShiftManagement.Api.Shared;

namespace ShiftManagement.Api.Modules.Identity.Application;

public static class IdentityErrors
{
    public static readonly Error UserNotFound =
        new(
            "identity.user.not_found",
            "User not found."
        );

    public static readonly Error EmailAlreadyInUse =
        new(
            "identity.user.email_already_in_use",
            "Email already in use."
        );

    public static readonly Error InvalidCredentials =
        new(
            "identity.auth.invalid_credentials",
            "Invalid credentials."
        );

    public static readonly Error UserInactive =
        new(
            "identity.user.inactive",
            "User is inactive."
        );
}