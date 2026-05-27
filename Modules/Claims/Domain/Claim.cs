using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using ShiftManagement.Api.Modules.Claims.Application;

namespace ShiftManagement.Api.Modules.Claims.Domain;

[Table("claims")]
public sealed class Claim
{
    [Key]
    public Guid Id { get; private set; }

    public Guid CompanyId { get; private set; }

    public Guid CollaboratorId { get; private set; }

    public ClaimReason Reason { get; private set; }

    public ClaimPriority Priority { get; private set; }

    public ClaimStatus Status { get; private set; }

    public string Title { get; private set; }

    public string Description { get; private set; }

    public Guid? AssignedToUserId { get; private set; }

    public DateTime CreatedAt { get; private set; }

    public DateTime? UpdatedAt { get; private set; }

    public DateTime? ResolvedAt { get; private set; }

    public DateTime? CanceledAt { get; private set; }

    private readonly List<ClaimComment> _comments;

    public IReadOnlyCollection<ClaimComment> Comments => _comments;

    private readonly List<ClaimEvidence> _evidences;

    public IReadOnlyCollection<ClaimEvidence> Evidences => _evidences;

    private Claim()
    {
        _comments = [];
        _evidences = [];
    }

    private Claim(
        Guid id,
        Guid companyId,
        Guid collaboratorId,
        ClaimReason reason,
        ClaimPriority priority,
        string title,
        string description,
        DateTime createdAt)
    {
        Id = id;
        CompanyId = companyId;
        CollaboratorId = collaboratorId;

        Reason = reason;
        Priority = priority;

        Title = title;
        Description = description;

        Status = ClaimStatus.Pending;

        CreatedAt = createdAt;

        _comments = [];
        _evidences = [];
    }

    public static Claim Create(
        Guid companyId,
        Guid collaboratorId,
        ClaimReason reason,
        ClaimPriority priority,
        string title,
        string description)
    {
        return new Claim(
            Guid.NewGuid(),
            companyId,
            collaboratorId,
            reason,
            priority,
            title,
            description,
            DateTime.UtcNow
        );
    }

    public void Update(
        string title,
        string description,
        ClaimPriority priority)
    {
        EnsureMutable();

        Title = title;
        Description = description;
        Priority = priority;

        UpdatedAt = DateTime.UtcNow;
    }

    public void Assign(Guid assignedToUserId)
    {
        EnsureMutable();

        AssignedToUserId = assignedToUserId;

        Status = ClaimStateMachine.Transition(
            Status,
            ClaimAction.StartReview
        );

        UpdatedAt = DateTime.UtcNow;
    }

    public void StartReview()
    {
        EnsureMutable();

        Status = ClaimStateMachine.Transition(
            Status,
            ClaimAction.StartReview
        );

        UpdatedAt = DateTime.UtcNow;
    }

    public void Resolve()
    {
        EnsureMutable();

        Status = ClaimStateMachine.Transition(
            Status,
            ClaimAction.Resolve
        );

        ResolvedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Reject()
    {
        EnsureMutable();

        Status = ClaimStateMachine.Transition(
            Status,
            ClaimAction.Reject
        );

        UpdatedAt = DateTime.UtcNow;
    }

    public void Cancel()
    {
        EnsureMutable();

        Status = ClaimStateMachine.Transition(
            Status,
            ClaimAction.Cancel
        );

        CanceledAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Reopen()
    {
        Status = ClaimStateMachine.Transition(Status, ClaimAction.Reopen);

        ResolvedAt = null;
        CanceledAt = null;

        UpdatedAt = DateTime.UtcNow;
    }

    public void AddComment(
    Guid authorUserId,
    string content,
    ClaimCommentType type = ClaimCommentType.General)
    {
        EnsureMutable();

        var comment = ClaimComment.Create(
            Id,
            authorUserId,
            content,
            type
        );

        _comments.Add(comment);

        UpdatedAt = DateTime.UtcNow;
    }

    public void AddEvidence(
        string fileName,
        string fileUrl,
        string mimeType)
    {
        EnsureMutable();

        var evidence = ClaimEvidence.Create(
            Id,
            fileName,
            fileUrl,
            mimeType
        );

        _evidences.Add(evidence);

        UpdatedAt = DateTime.UtcNow;
    }

    private void EnsureMutable()
    {
        if (ClaimStateMachine.IsTerminal(Status))
            throw ClaimExceptions.ClaimAlreadyClosed(Id);
    }
}