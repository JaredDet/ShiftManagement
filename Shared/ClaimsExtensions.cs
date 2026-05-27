using System.Security.Claims;

namespace ShiftManagement.Api.Shared;

public static class ClaimsExtensions
{
    public static Guid GetCompanyId(this ClaimsPrincipal user)
    {
        var value = user.FindFirst("companyId")?.Value;

        if (string.IsNullOrWhiteSpace(value))
            throw new InvalidOperationException("Missing companyId claim");

        return Guid.Parse(value);
    }

    public static Guid GetUserId(this ClaimsPrincipal user)
    {
        var value = user.FindFirst(ClaimTypes.NameIdentifier)?.Value
                    ?? user.FindFirst("sub")?.Value;

        if (string.IsNullOrWhiteSpace(value))
            throw new InvalidOperationException("Missing userId claim");

        return Guid.Parse(value);
    }
}