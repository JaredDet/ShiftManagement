namespace ShiftManagement.Api.Modules.Claims.Api.Contracts.Queries;

public sealed class ListClaimsRequest
{
    public Guid? CollaboratorId { get; init; }

    public string? Status { get; init; }

    public string? Reason { get; init; }

    public string? Priority { get; init; }

    public Guid? AssignedToUserId { get; init; }

    public DateTime? CreatedFrom { get; init; }

    public DateTime? CreatedTo { get; init; }
}