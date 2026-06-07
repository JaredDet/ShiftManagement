using ShiftManagement.Api.BuildingBlocks.Exceptions;

namespace ShiftManagement.Api.Modules.Contracts.Application;

public static class ContractErrors
{
    public static readonly Error ContractNotFound =
        new("contract.not_found", "Contract not found.", ErrorType.NotFound);

    public static Error ActiveContractNotFound(Guid collaboratorId)
    {
        return new(
            "contracts.active_not_found",
            $"Active contract not found for collaborator '{collaboratorId}'.",
            ErrorType.NotFound
        );
    }
}