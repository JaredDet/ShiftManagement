using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using ShiftManagement.Api.Modules.Contracts.Domain.Enums;

namespace ShiftManagement.Api.Modules.Contracts.Domain;

[Index(nameof(CollaboratorId))]
[Table("employment_contracts")]
public class EmploymentContract
{
    [Key]
    public Guid Id { get; private set; }

    public Guid CollaboratorId { get; private set; }

    public Guid CreatedByUserId { get; private set; }

    public ContractType Type { get; private set; }

    public ContractStatus Status { get; private set; }

    public WorkScheduleType WorkScheduleType { get; private set; }

    public decimal SalaryAmount { get; private set; }

    public Currency Currency { get; private set; }

    public PayPeriod PayPeriod { get; private set; }

    public DateOnly StartDate { get; private set; }

    public DateOnly? EndDate { get; private set; }

    public DateTime CreatedAt { get; private set; }

    public ContractTermination? Termination { get; private set; }

    private EmploymentContract() { }

    private EmploymentContract(
        Guid id,
        Guid collaboratorId,
        Guid createdByUserId,
        ContractType type,
        ContractStatus status,
        WorkScheduleType workScheduleType,
        decimal salaryAmount,
        Currency currency,
        PayPeriod payPeriod,
        DateOnly startDate,
        DateOnly? endDate,
        DateTime createdAt)
    {
        Id = id;
        CollaboratorId = collaboratorId;
        CreatedByUserId = createdByUserId;
        Type = type;
        Status = status;
        WorkScheduleType = workScheduleType;
        SalaryAmount = salaryAmount;
        Currency = currency;
        PayPeriod = payPeriod;
        StartDate = startDate;
        EndDate = endDate;
        CreatedAt = createdAt;
    }

    public static EmploymentContract Create(
        Guid collaboratorId,
        Guid createdByUserId,
        ContractType type,
        WorkScheduleType workScheduleType,
        decimal salaryAmount,
        Currency currency,
        PayPeriod payPeriod,
        DateOnly startDate,
        DateOnly? endDate)
    {
        return new EmploymentContract(
            Guid.NewGuid(),
            collaboratorId,
            createdByUserId,
            type,
            ContractStatus.ACTIVE,
            workScheduleType,
            salaryAmount,
            currency,
            payPeriod,
            startDate,
            endDate,
            DateTime.UtcNow
        );
    }

    public void Terminate(
        DateOnly terminationDate,
        ContractTerminationReason reason,
        string? comment,
        Guid terminatedByUserId,
        string? settlementDocumentUrl)
    {
        if (Status == ContractStatus.TERMINATED)
            throw ContractExceptions.AlreadyTerminated(Id);

        Status = ContractStatus.TERMINATED;
        EndDate = terminationDate;

        Termination = ContractTermination.Create(
            Id,
            reason,
            comment,
            terminatedByUserId,
            settlementDocumentUrl,
            terminationDate
        );
    }
}