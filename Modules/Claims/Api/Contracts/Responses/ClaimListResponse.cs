namespace ShiftManagement.Api.Modules.Claims.Api.Contracts.Responses;

public sealed record ClaimListResponse
{
    public IReadOnlyList<ClaimResponse> Claims { get; init; } = [];
}