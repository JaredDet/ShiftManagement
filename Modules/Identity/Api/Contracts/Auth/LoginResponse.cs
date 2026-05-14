using ShiftManagement.Api.Modules.Identity.Api.Contracts.Users;

namespace ShiftManagement.Api.Modules.Identity.Api.Contracts.Auth;

public sealed record LoginResponse
{
    public string AccessToken { get; set; } = null!;
    public UserResponse User { get; set; } = null!;
}