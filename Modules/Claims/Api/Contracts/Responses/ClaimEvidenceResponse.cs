namespace ShiftManagement.Api.Modules.Claims.Api.Contracts.Responses;

public sealed record ClaimEvidenceResponse
{
    public Guid Id { get; init; }

    public Guid ClaimId { get; init; }

    public string FileName { get; init; } = string.Empty;

    public string FileUrl { get; init; } = string.Empty;

    public string MimeType { get; init; } = string.Empty;

    public DateTime CreatedAt { get; init; }
}
