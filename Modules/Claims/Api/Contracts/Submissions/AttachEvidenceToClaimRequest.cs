namespace ShiftManagement.Api.Modules.Claims.Api.Contracts.Submissions;

public sealed class AttachEvidenceToClaimRequest
{
    public Guid ClaimId { get; init; }

    public string FileName { get; init; } = string.Empty;

    public string MimeType { get; init; } = string.Empty;

    public string Base64Content { get; init; } = string.Empty;
}