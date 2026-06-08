using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShiftManagement.Api.BuildingBlocks.Results;
using ShiftManagement.Api.Infrastructure.Auth;
using ShiftManagement.Api.Modules.Contracts.Api.Contracts.Managements;
using ShiftManagement.Api.Modules.Contracts.Application.Commands;
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
    [Authorize(Policy = AuthorizationPolicies.ContractsWriteAccess)]
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
    [Authorize(Policy = AuthorizationPolicies.ContractsWriteAccess)]
    public async Task<IActionResult> TerminateContract(
    Guid contractId,
    [FromBody] TerminateEmploymentContractRequest request)
    {
        return (await terminate.ExecuteAsync(contractId, request))
            .Match(NoContent);
    }

    [HttpGet("{contractId:guid}")]
    [Authorize(Policy = AuthorizationPolicies.ContractsReadAccess)]
    public async Task<IActionResult> GetContract(
        Guid contractId)
    {
        return (await get.ExecuteAsync(contractId))
            .Match(Ok);
    }

    [HttpGet("{contractId:guid}/pdf")]
    [Authorize(Policy = AuthorizationPolicies.ContractsExportAccess)]
    public async Task<IActionResult> DownloadContractPdf(
    Guid contractId)
    {
        return (await downloadPdf.ExecuteAsync(
                contractId))
            .Match(file =>
                File(
                    file.Document,
                        file.ContentType,
                        file.FileName));
    }

    [HttpGet("me")]
    [Authorize(Policy = AuthorizationPolicies.ContractsSelfAccess)]
    public async Task<IActionResult> GetMyContract()
    {
        return (await getMyContract.ExecuteAsync())
            .Match(Ok);
    }
}