using ShiftManagement.Api.Modules.Staff.Api.Contracts.Collaborators;
using ShiftManagement.Api.Modules.Staff.Domain;

namespace ShiftManagement.Api.Modules.Staff.Application.Mappers;

public static class CollaboratorMapper
{
    public static CollaboratorResponse ToResponse(Collaborator collaborator)
    {
        return new CollaboratorResponse
        {
            Id = collaborator.Id,
            UserId = collaborator.UserId,
            CompanyId = collaborator.CompanyId,
            Status = collaborator.Status.ToString(),
            CreatedAt = collaborator.CreatedAt,

            Name = "",
            Email = "",
            MainBranchName = "",
            MainPositionName = ""
        };
    }
}