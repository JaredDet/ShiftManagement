using System.Security.Claims;
using ShiftManagement.Api.BuildingBlocks.Execution;

namespace ShiftManagement.Api.Infrastructure.Execution;

public sealed class HttpExecutionContext(IHttpContextAccessor accessor)
    : IExecutionContext
{
    private ClaimsPrincipal User =>
        accessor.HttpContext?.User
        ?? throw new InvalidOperationException("No HTTP context");

    public Guid UserId =>
        Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)
                   ?? User.FindFirstValue("sub")
                   ?? throw new InvalidOperationException("Missing user id"));

    public Guid CompanyId =>
        Guid.Parse(User.FindFirstValue("companyId")
                   ?? throw new InvalidOperationException("Missing company id"));

    public string? Email => User.FindFirstValue(ClaimTypes.Email);
}