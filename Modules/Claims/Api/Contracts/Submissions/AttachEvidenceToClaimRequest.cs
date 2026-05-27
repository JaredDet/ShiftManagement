using System.ComponentModel.DataAnnotations;
using ShiftManagement.Api.Shared;

namespace ShiftManagement.Api.Modules.Claims.Api.Contracts.Submissions;

public sealed record AttachEvidenceToClaimRequest
{
    [Required]
    [MaxLength(255)]
    public string FileName { get; init; } = string.Empty;

    [Required]
    [MaxLength(100)]
    public string MimeType { get; init; } = string.Empty;

    [Required]
    public string Base64Content { get; init; } = string.Empty;
}