namespace ShiftManagement.Api.Modules.Claims.Api.Contracts.Responses;

public sealed class ClaimListResponse
{
    public IReadOnlyList<ClaimResponse> Claims { get; init; } = [];
}