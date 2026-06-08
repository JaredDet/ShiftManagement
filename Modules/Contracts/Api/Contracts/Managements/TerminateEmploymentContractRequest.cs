using System.ComponentModel.DataAnnotations;
using ShiftManagement.Api.Modules.Contracts.Domain;
using ShiftManagement.Api.Modules.Contracts.Domain.Enums;
using ShiftManagement.Api.Validations;

namespace ShiftManagement.Api.Modules.Contracts.Api.Contracts.Managements;

public sealed record TerminateEmploymentContractRequest
{
    [NotEmptyGuid]
    public Guid ContractId { get; init; }

    [EnumDataType(typeof(ContractTerminationReason))]
    public ContractTerminationReason Reason { get; init; }

    public DateOnly TerminationDate { get; init; }

    public string? Comment { get; init; }
}