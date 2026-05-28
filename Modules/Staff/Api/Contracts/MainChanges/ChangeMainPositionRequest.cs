using ShiftManagement.Api.Validations;

namespace ShiftManagement.Api.Modules.Staff.Api.Contracts.MainChanges;

public sealed record ChangeMainPositionRequest
{
    [NotEmptyGuid]
    public Guid PositionId { get; set; }
}