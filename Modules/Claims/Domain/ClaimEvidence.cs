using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShiftManagement.Api.Modules.Claims.Domain;

[Table("claim_evidence")]
public sealed class ClaimEvidence
{
    [Key]
    public Guid Id { get; private set; }

    public Guid ClaimId { get; private set; }

    [MaxLength(255)]
    public string FileName { get; private set; } = string.Empty;

    [MaxLength(1000)]
    public string FileUrl { get; private set; } = string.Empty;

    [MaxLength(255)]
    public string MimeType { get; private set; } = string.Empty;

    public DateTime CreatedAt { get; private set; }

    [ForeignKey(nameof(ClaimId))]
    public Claim Claim { get; private set; } = null!;

    private ClaimEvidence() { }

    private ClaimEvidence(
        Guid id,
        Guid claimId,
        string fileName,
        string fileUrl,
        string mimeType,
        DateTime createdAt)
    {
        Id = id;
        ClaimId = claimId;

        FileName = fileName;
        FileUrl = fileUrl;
        MimeType = mimeType;

        CreatedAt = createdAt;
    }

    public static ClaimEvidence Create(
        Guid claimId,
        string fileName,
        string fileUrl,
        string mimeType)
    {
        return new ClaimEvidence(
            Guid.NewGuid(),
            claimId,
            fileName,
            fileUrl,
            mimeType,
            DateTime.UtcNow);
    }
}