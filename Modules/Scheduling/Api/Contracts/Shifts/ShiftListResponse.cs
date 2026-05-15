namespace ShiftManagement.Api.Modules.Scheduling.Api.Contracts;

public sealed record ShiftListResponse
{
    public List<ShiftResponse> Shifts { get; init; }
}