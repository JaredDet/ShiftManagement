using ShiftManagement.Api.Shared;

namespace ShiftManagement.Api.Modules.Staff.Api.Contracts.MainChanges;

public sealed record ChangeMainBranchRequest
{
    [NotEmptyGuid]
    public Guid BranchId { get; set; }
}