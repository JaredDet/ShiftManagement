namespace ShiftManagement.Api.Modules.Contracts.Api.Contracts.Documents;

public sealed record DownloadContractPdfResponse
{
    public Guid ContractId { get; init; }

    public string FileName { get; init; } = string.Empty;

    public string FileUrl { get; init; } = string.Empty;
}