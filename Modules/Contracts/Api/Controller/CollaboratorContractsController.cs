using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShiftManagement.Api.BuildingBlocks.Results;
using ShiftManagement.Api.Modules.Contracts.Application;
using ShiftManagement.Api.Modules.Contracts.Application.Queries;

namespace ShiftManagement.Api.Modules.Contracts.Api.Controllers;

[ApiController]
[Authorize]
[Route("api/collaborators/{collaboratorId:guid}/contracts")]
public sealed class CollaboratorContractsController(
    GetActiveContractUseCase getActiveContract,
    ListCollaboratorContractsUseCase listCollaboratorContracts)
    : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> ListContracts(
        Guid collaboratorId)
    {
        return (await listCollaboratorContracts.ExecuteAsync(collaboratorId))
            .Match(Ok);
    }

    [HttpGet("active")]
    public async Task<IActionResult> GetActiveContract(
        Guid collaboratorId)
    {
        return (await getActiveContract.ExecuteAsync(collaboratorId))
            .Match(Ok);
    }
}