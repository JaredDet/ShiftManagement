using ShiftManagement.Api.Modules.Contracts.Api.Contracts.Queries;

namespace ShiftManagement.Api.Modules.Contracts.Application;

public static class EmploymentContractPdfMapper
{
    public static EmploymentContractPdfModel ToPdfModel(EmploymentContractResponse contract)
    {
        return new EmploymentContractPdfModel
        {
            Id = contract.Id,
            CollaboratorName = contract.CollaboratorName,
            ApprovedByName = contract.ApprovedByName,
            Type = contract.Type,
            Status = contract.Status,
            WorkScheduleType = contract.WorkScheduleType,
            SalaryAmount = contract.SalaryAmount,
            Currency = contract.Currency,
            PayPeriod = contract.PayPeriod,
            StartsAt = contract.StartsAt,
            EndsAt = contract.EndsAt,
            CreatedAt = contract.CreatedAt,
            TerminationReason = contract.Termination?.Reason
        };
    }
}