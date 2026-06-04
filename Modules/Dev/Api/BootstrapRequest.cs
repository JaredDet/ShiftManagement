namespace ShiftManagement.Api.Modules.Dev.Api;

public sealed record BootstrapCompanyRequest(
    string CompanyName,
    string AdminName,
    string AdminEmail,
    string Password
);