using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShiftManagement.Api.BuildingBlocks.Results;
using ShiftManagement.Api.Modules.Contracts.Api.Contracts.Managements;
using ShiftManagement.Api.Modules.Contracts.Application;
using ShiftManagement.Api.Modules.Contracts.Application.Command;
using ShiftManagement.Api.Modules.Contracts.Application.Queries;

namespace ShiftManagement.Api.Modules.Contracts.Api.Controllers;

[ApiController]
[Authorize]
[Route("api/contracts")]
public sealed class ContractController(
    CreateContractUseCase create,
    TerminateContractUseCase terminate,
    GetContractUseCase get,
    GetMyContractUseCase getMyContract,
    DownloadContractPdfUseCase downloadPdf)
    : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateContract(
    [FromBody] CreateEmploymentContractRequest request)
    {
        return (await create.ExecuteAsync(request))
            .Match(contractId =>
                CreatedAtAction(
                    nameof(GetContract),
                    new { contractId },
                    new { id = contractId }
                )
            );
    }

    [HttpPost("{contractId:guid}/terminate")]
    public async Task<IActionResult> TerminateContract(
    Guid contractId,
    [FromBody] TerminateEmploymentContractRequest request)
    {
        return (await terminate.ExecuteAsync(contractId, request))
            .Match(NoContent);
    }

    [HttpGet("{contractId:guid}")]
    public async Task<IActionResult> GetContract(
        Guid contractId)
    {
        return (await get.ExecuteAsync(contractId))
            .Match(Ok);
    }

    [HttpGet("{contractId:guid}/pdf")]
    public async Task<IActionResult> DownloadContractPdf(
    Guid contractId,
    CancellationToken cancellationToken)
    {
        return (await downloadPdf.ExecuteAsync(
                contractId,
                cancellationToken))
            .Match(file =>
                File(
                    file.Content,
                    "application/pdf",
                    file.FileName));
    }

    [HttpGet("me")]
    public async Task<IActionResult> GetMyContract()
    {
        return (await getMyContract.ExecuteAsync())
            .Match(Ok);
    }
}