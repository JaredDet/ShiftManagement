using ShiftManagement.Api.Modules.Claims.Api.Contracts.Responses;
using ShiftManagement.Api.Modules.Claims.Domain;

namespace ShiftManagement.Api.Modules.Claims.Application;

public static class ClaimMapper
{
    public static ClaimResponse ToResponse(Claim claim)
    {
        return new ClaimResponse
        {
            Id = claim.Id,
            CompanyId = claim.CompanyId,
            CollaboratorId = claim.CollaboratorId,

            Reason = claim.Reason.ToString(),
            Priority = claim.Priority.ToString(),
            Status = claim.Status.ToString(),

            Title = claim.Title,
            Description = claim.Description,

            AssignedToUserId = claim.AssignedToUserId,

            CreatedAt = claim.CreatedAt,
            UpdatedAt = claim.UpdatedAt,
            ResolvedAt = claim.ResolvedAt,
            CanceledAt = claim.CanceledAt,

            CommentsCount = claim.Comments.Count,
            EvidencesCount = claim.Evidences.Count,

            IsClosed = ClaimStateMachine.IsTerminal(claim.Status)
        };
    }
}