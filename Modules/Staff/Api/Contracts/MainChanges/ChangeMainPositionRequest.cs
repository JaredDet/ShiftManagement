using ShiftManagement.Api.Shared;

namespace ShiftManagement.Api.Modules.Staff.Api.Contracts.MainChanges;

public sealed record ChangeMainPositionRequest
{
    [NotEmptyGuid]
    public Guid PositionId { get; set; }
}