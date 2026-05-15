namespace ShiftManagement.Api.Modules.Scheduling.Api.Contracts;

public sealed record ShiftSwapRequestListResponse
{
    public List<ShiftSwapRequestResponse> Requests { get; init; }
}