using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using ShiftManagement.Api.Modules.Contracts.Domain.Enums;

namespace ShiftManagement.Api.Modules.Contracts.Domain;

[Index(nameof(ContractId))]
[Table("contract_terminations")]
public class ContractTermination
{
    [Key]
    public Guid Id { get; private set; }

    public Guid ContractId { get; private set; }

    public ContractTerminationReason Reason { get; private set; }

    public string? Comment { get; private set; }

    public Guid TerminatedByUserId { get; private set; }

    public string? SettlementDocumentUrl { get; private set; }

    public DateOnly TerminationDate { get; private set; }

    public DateTime CreatedAt { get; private set; }

    private ContractTermination() { }

    private ContractTermination(
        Guid id,
        Guid contractId,
        ContractTerminationReason reason,
        string? comment,
        Guid terminatedByUserId,
        string? settlementDocumentUrl,
        DateOnly terminationDate,
        DateTime createdAt)
    {
        Id = id;
        ContractId = contractId;
        Reason = reason;
        Comment = comment;
        TerminatedByUserId = terminatedByUserId;
        SettlementDocumentUrl = settlementDocumentUrl;
        TerminationDate = terminationDate;
        CreatedAt = createdAt;
    }

    public static ContractTermination Create(
        Guid contractId,
        ContractTerminationReason reason,
        string? comment,
        Guid terminatedByUserId,
        string? settlementDocumentUrl,
        DateOnly terminationDate)
    {
        return new ContractTermination(
            Guid.NewGuid(),
            contractId,
            reason,
            comment,
            terminatedByUserId,
            settlementDocumentUrl,
            terminationDate,
            DateTime.UtcNow
        );
    }
}