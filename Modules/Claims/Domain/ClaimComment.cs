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

    public ClaimCommentType Type { get; private set; }

    public string Content { get; private set; } = string.Empty;

    public DateTime CreatedAt { get; private set; }

    private ClaimComment() { }

    private ClaimComment(
        Guid id,
        Guid claimId,
        Guid authorUserId,
        string content,
        ClaimCommentType type,
        DateTime createdAt)
    {
        Id = id;
        ClaimId = claimId;
        AuthorUserId = authorUserId;
        Content = content;
        Type = type;
        CreatedAt = createdAt;
    }

    public static ClaimComment Create(
        Guid claimId,
        Guid authorUserId,
        string content,
        ClaimCommentType type)
    {
        return new ClaimComment(
            Guid.NewGuid(),
            claimId,
            authorUserId,
            content,
            type,
            DateTime.UtcNow);
    }
}