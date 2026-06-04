namespace ShiftManagement.Api.Modules.Session.Api.Contracts;

public sealed class SessionResponse
{
    public Guid UserId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;

    public CompanyResponse Company { get; set; } = default!;

    public MainBranchResponse MainBranch { get; set; } = default!;

    public string Role { get; set; } = string.Empty;
}