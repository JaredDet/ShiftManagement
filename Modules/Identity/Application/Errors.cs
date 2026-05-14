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

    public static readonly Error RoleNotFound =
        new(
            "identity.role.not_found",
            "Role not found."
        );

    public static readonly Error RoleAlreadyAssigned =
        new(
            "identity.role.already_assigned",
            "Role already assigned to user."
        );

    public static readonly Error BranchRequiredForRole =
    new(
        "identity.role.branch_required",
        "BranchId is required for this role."
        );

    public static readonly Error BranchNotAllowedForRole =
        new(
            "identity.role.branch_not_allowed",
            "BranchId is not allowed for this role."
        );
}