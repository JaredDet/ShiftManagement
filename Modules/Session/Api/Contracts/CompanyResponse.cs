namespace ShiftManagement.Api.Modules.Session.Api.Contracts;

public sealed class CompanyResponse
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;
}