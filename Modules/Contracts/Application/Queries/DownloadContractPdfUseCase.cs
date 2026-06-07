using QuestPDF.Fluent;
using ShiftManagement.Api.BuildingBlocks.Results;
using ShiftManagement.Api.Modules.Contracts.Api.Contracts.Documents;
using ShiftManagement.Api.Modules.Contracts.Infrastructure;
using ShiftManagement.Api.Modules.Contracts.Infrastructure.Pdf;

namespace ShiftManagement.Api.Modules.Contracts.Application.Queries;

public sealed class DownloadContractPdfUseCase(
    ContractReadRepository repository,
    ContractPdfBuilder builder)
{
    public async Task<Result<DownloadContractPdfResponse>> ExecuteAsync(Guid contractId)
    {
        var contract = await repository.GetByIdAsync(contractId);

        if (contract is null)
        {
            return Result<DownloadContractPdfResponse>.Failure(
                ContractErrors.ContractNotFound);
        }

        var model = EmploymentContractPdfMapper.ToPdfModel(contract);

        var document = builder.Build(model);

        var bytes = document.GeneratePdf();

        var fileName = BuildFileName(model.CollaboratorName, contractId);

        return Result<DownloadContractPdfResponse>.Success(
            new DownloadContractPdfResponse
            {
                FileName = fileName,
                ContentType = "application/pdf",
                Document = bytes
            });
    }

    private static string BuildFileName(string collaboratorName, Guid contractId)
    {
        var safeName = string.Concat(collaboratorName
            .Normalize(System.Text.NormalizationForm.FormD)
            .Where(c => char.IsLetterOrDigit(c) || char.IsWhiteSpace(c)))
            .Trim()
            .Replace(" ", "_");

        return $"Contrato_{safeName}_{contractId}.pdf";
    }
}