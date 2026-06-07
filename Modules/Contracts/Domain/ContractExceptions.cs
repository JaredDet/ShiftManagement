using ShiftManagement.Api.BuildingBlocks.Exceptions;

namespace ShiftManagement.Api.Modules.Contracts.Domain;

public static class ContractExceptions
{
    public static DomainException AlreadyTerminated(Guid contractId)
    {
        return new DomainException(
            "contracts.already_terminated",
            $"Contract '{contractId}' is already terminated",
            ErrorType.Conflict
        );
    }
}