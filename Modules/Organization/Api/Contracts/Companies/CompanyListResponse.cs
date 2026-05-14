namespace ShiftManagement.Api.Modules.Organization.Api.Contracts.Companies;

public sealed record CompanyListResponse(
    List<CompanyResponse> Companies
);