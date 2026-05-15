namespace ShiftManagement.Api.Modules.Scheduling.Domain;

public enum ShiftSwapStatus
{
    Pending,
    AcceptedByCollaborator,
    Approved,
    Rejected,
    Cancelled
}