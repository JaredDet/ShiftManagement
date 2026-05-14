namespace ShiftManagement.Api.Modules.Staff.Api.Contracts.Positions;

public sealed record PositionListResponse
{
    public List<PositionResponse> Positions { get; set; } = new();
}