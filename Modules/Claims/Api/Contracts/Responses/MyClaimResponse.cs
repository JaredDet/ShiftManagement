namespace ShiftManagement.Api.Modules.Claims.Api.Contracts.Responses;

public sealed record MyClaimResponse
{
    public Guid Id { get; init; }

    public string Title { get; init; } = string.Empty;

    public string Reason { get; init; } = string.Empty;

    public string Status { get; init; } = string.Empty;

    public DateTime CreatedAt { get; init; }
}