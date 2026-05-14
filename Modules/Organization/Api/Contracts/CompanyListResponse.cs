namespace ShiftManagement.Api.Modules.Organization.Api.Contracts;

public sealed record CompanyListResponse(
    List<CompanyResponse> Companies
);