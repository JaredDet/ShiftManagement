using ShiftManagement.Api.Modules.Staff.Api.Contracts.Positions;
using ShiftManagement.Api.Modules.Staff.Domain;

namespace ShiftManagement.Api.Modules.Staff.Application.Mappers;

public static class PositionMapper
{
    public static PositionResponse ToResponse(Position position)
    {
        return new PositionResponse
        {
            Id = position.Id,
            Name = position.Name,
            CompanyId = position.CompanyId,
            Status = position.Status.ToString(),
            CreatedAt = position.CreatedAt
        };
    }
}