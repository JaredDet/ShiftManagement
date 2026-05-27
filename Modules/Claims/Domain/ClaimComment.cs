using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShiftManagement.Api.Modules.Claims.Domain;

[Table("claim_comment")]
public sealed class ClaimComment
{
    [Key]
    public Guid Id { get; private set; }

    public Guid ClaimId { get; private set; }

    public Guid AuthorUserId { get; private set; }

    [MaxLength(2000)]
    public string Content { get; private set; } = string.Empty;

    public DateTime CreatedAt { get; private set; }

    [ForeignKey(nameof(ClaimId))]
    public Claim Claim { get; private set; } = null!;

    private ClaimComment() { }

    private ClaimComment(
        Guid id,
        Guid claimId,
        Guid authorUserId,
        string content,
        DateTime createdAt)
    {
        Id = id;
        ClaimId = claimId;
        AuthorUserId = authorUserId;
        Content = content;
        CreatedAt = createdAt;
    }

    public static ClaimComment Create(
        Guid claimId,
        Guid authorUserId,
        string content)
    {
        return new ClaimComment(
            Guid.NewGuid(),
            claimId,
            authorUserId,
            content,
            DateTime.UtcNow);
    }
}