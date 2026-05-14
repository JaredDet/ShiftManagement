using ShiftManagement.Api.Modules.Organization.Api.Contracts;
using ShiftManagement.Api.Modules.Organization.Domain;

namespace ShiftManagement.Api.Modules.Organization.Application.Companies;

public static class CompanyMapper
{
    public static CompanyResponse ToResponse(Company company)
    {
        return new CompanyResponse(
            company.Id,
            company.Name,
            company.Status.ToString().ToLower(),
            company.CreatedAt
        );
    }
}