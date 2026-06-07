using System.ComponentModel.DataAnnotations;
using ShiftManagement.Api.Modules.Contracts.Domain;
using ShiftManagement.Api.Validations;

namespace ShiftManagement.Api.Modules.Contracts.Api.Contracts.Managements;

public sealed record CreateEmploymentContractRequest
{
    [NotEmptyGuid]
    public Guid CollaboratorId { get; init; }

    [EnumDataType(typeof(ContractType))]
    public ContractType Type { get; init; }

    [EnumDataType(typeof(WorkScheduleType))]
    public WorkScheduleType WorkScheduleType { get; init; }

    [Range(typeof(decimal), "0.01", "999999999999.99")]
    public decimal SalaryAmount { get; init; }

    [EnumDataType(typeof(Currency))]
    public Currency Currency { get; init; }

    [EnumDataType(typeof(PayPeriod))]
    public PayPeriod PayPeriod { get; init; }

    public DateOnly StartsAt { get; init; }

    [DateRange(nameof(StartsAt))]
    public DateOnly? EndsAt { get; init; }
}