namespace ShiftManagement.Api.Modules.Claims.Api.Contracts.Submissions;

public sealed class UpdateClaimRequest
{
    public string Title { get; init; } = string.Empty;

    public string Description { get; init; } = string.Empty;

    public string Priority { get; init; } = string.Empty;
}