namespace ShiftManagement.Api.Modules.Claims.Api.Contracts.Responses;

public sealed record ClaimEvidencesResponse
{
    public Guid ClaimId { get; init; }

    public IReadOnlyList<ClaimEvidenceResponse> Evidences { get; init; } = [];
}