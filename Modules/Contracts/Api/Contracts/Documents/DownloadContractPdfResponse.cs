namespace ShiftManagement.Api.Modules.Contracts.Api.Contracts.Documents;

public sealed record DownloadContractPdfResponse
{
    public string FileName { get; init; } = string.Empty;
    public string ContentType { get; init; } = "application/pdf";

    public required byte[] Document { get; init; }
}