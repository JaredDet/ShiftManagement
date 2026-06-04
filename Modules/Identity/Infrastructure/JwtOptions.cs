namespace ShiftManagement.Api.Modules.Identity.Infrastructure;

public sealed class JwtOptions
{
    public string Key { get; set; } = default!;
    public string Issuer { get; set; } = default!;
    public string Audience { get; set; } = default!;
    public int ExpiresInMinutes { get; set; } = default!;
}