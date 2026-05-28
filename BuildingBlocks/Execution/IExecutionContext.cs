namespace ShiftManagement.Api.BuildingBlocks.Execution;

public interface IExecutionContext
{
    Guid UserId { get; }
    Guid CompanyId { get; }
}