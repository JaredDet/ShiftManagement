using Microsoft.AspNetCore.Authorization;
using ShiftManagement.Api.Modules.Identity.Domain;

namespace ShiftManagement.Api.Infrastructure.Auth;

public static class AuthorizationPolicies
{
    public const string CompanyAdminOnly = "CompanyAdminOnly";
    public const string BranchManagerOnly = "BranchManagerOnly";
    public const string SupervisorOnly = "SupervisorOnly";
    public const string StaffOnly = "StaffOnly";

    public const string StaffReadAccess = "StaffReadAccess";
    public const string SwapModerationAccess = "SwapModerationAccess";
    public const string ShiftManagementAccess = "ShiftManagementAccess";
    public const string SwapParticipantAccess = "SwapParticipantAccess";
    public const string SwapRejectAccess = "SwapRejectAccess";

    public static void AddPolicies(AuthorizationOptions options)
    {
        options.AddPolicy(CompanyAdminOnly,
            p => p.RequireRole(Role.CompanyAdmin.ToString()));

        options.AddPolicy(BranchManagerOnly,
            p => p.RequireRole(Role.BranchManager.ToString()));

        options.AddPolicy(SupervisorOnly,
            p => p.RequireRole(Role.Supervisor.ToString()));

        options.AddPolicy(StaffOnly,
            p => p.RequireRole(Role.Staff.ToString()));

        options.AddPolicy(StaffReadAccess,
            p => p.RequireRole(
                Role.CompanyAdmin.ToString(),
                Role.BranchManager.ToString(),
                Role.Supervisor.ToString()
            ));

        options.AddPolicy(SwapModerationAccess,
            p => p.RequireRole(
                Role.CompanyAdmin.ToString(),
                Role.BranchManager.ToString(),
                Role.Supervisor.ToString()
            ));

        options.AddPolicy(ShiftManagementAccess,
            p => p.RequireRole(
                Role.BranchManager.ToString(),
                Role.Supervisor.ToString()
            ));

        options.AddPolicy(SwapParticipantAccess,
            p => p.RequireRole(Role.Staff.ToString()));

        options.AddPolicy(SwapRejectAccess,
            p => p.RequireRole(
                Role.Staff.ToString(),
                Role.Supervisor.ToString(),
                Role.BranchManager.ToString(),
                Role.CompanyAdmin.ToString()
            ));
    }
}