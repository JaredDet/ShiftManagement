namespace ShiftManagement.Api.Modules.Staff.Api.Contracts.Positions;

public sealed record PositionResponse
{
    public Guid Id { get; set; }
    public Guid CompanyId { get; set; }

    public string Name { get; set; }
    public string? Description { get; set; }

    public string Status { get; set; }

    public DateTime CreatedAt { get; set; }
}