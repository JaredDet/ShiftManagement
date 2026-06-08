using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShiftManagement.Api.BuildingBlocks.Results;
using ShiftManagement.Api.Infrastructure.Auth;
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
    [Authorize(Policy = AuthorizationPolicies.ContractsReadAccess)]
    public async Task<IActionResult> ListContracts(
        Guid collaboratorId)
    {
        return (await listCollaboratorContracts.ExecuteAsync(collaboratorId))
            .Match(Ok);
    }

    [HttpGet("active")]
    [Authorize(Policy = AuthorizationPolicies.ContractsReadAccess)]
    public async Task<IActionResult> GetActiveContract(
        Guid collaboratorId)
    {
        return (await getActiveContract.ExecuteAsync(collaboratorId))
            .Match(Ok);
    }
}