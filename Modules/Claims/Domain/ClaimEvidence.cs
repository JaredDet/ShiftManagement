using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShiftManagement.Api.Modules.Claims.Domain;

[Table("claim_evidence")]
public sealed class ClaimEvidence
{
    [Key]
    public Guid Id { get; private set; }

    public Guid ClaimId { get; private set; }

    public string FileName { get; private set; } = string.Empty;

    public string FileUrl { get; private set; } = string.Empty;

    public string MimeType { get; private set; } = string.Empty;

    public DateTime CreatedAt { get; private set; }

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