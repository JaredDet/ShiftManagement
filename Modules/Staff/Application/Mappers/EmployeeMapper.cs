using ShiftManagement.Api.Modules.Staff.Api.Contracts.Collaborators;
using ShiftManagement.Api.Modules.Staff.Domain;

namespace ShiftManagement.Api.Modules.Staff.Application.Mappers;

public static class EmployeeMapper
{
    public static CollaboratorResponse ToResponse(Employee employee)
    {
        return new CollaboratorResponse
        {
            Id = employee.Id,
            UserId = employee.UserId,
            CompanyId = employee.CompanyId,
            Status = employee.Status.ToString(),
            CreatedAt = employee.CreatedAt,

            Name = "",
            Email = "",
            MainBranchName = "",
            MainPositionName = ""
        };
    }
}