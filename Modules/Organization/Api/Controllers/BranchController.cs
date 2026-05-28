using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShiftManagement.Api.BuildingBlocks.Results;
using ShiftManagement.Api.Modules.Organization.Api.Contracts.Branches;
using ShiftManagement.Api.Modules.Organization.Application.Branches;

namespace ShiftManagement.Api.Modules.Organization.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/branches")]
public class BranchController(
    CreateBranchUseCase create,
    GetBranchUseCase get,
    UpdateBranchUseCase update,
    DeactivateBranchUseCase deactivate,
    ListBranchesUseCase list
) : ControllerBase
{
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetBranch(Guid id)
    {
        return (await get.ExecuteAsync(id))
            .Match(Ok);
    }

    [HttpGet]
    public async Task<IActionResult> ListBranches([FromQuery] Guid companyId)
    {
        return (await list.ExecuteAsync(companyId))
            .Match(Ok);
    }

    [HttpPost]
    [Authorize(Policy = "CompanyAdminOnly")]
    public async Task<IActionResult> CreateBranch(
            CreateBranchRequest request
        )
    {
        return (await create.ExecuteAsync(request))
            .Match(value =>
                CreatedAtAction(
                    nameof(GetBranch),
                    new { id = value.Id },
                    value
                )
            );
    }

    [HttpPut("{id:guid}")]
    [Authorize(Policy = "CompanyAdminOnly")]
    public async Task<IActionResult> UpdateBranch(
        Guid id,
        UpdateBranchRequest request
    )
    {
        return (await update.ExecuteAsync(id, request))
            .Match(Ok);
    }

    [HttpPatch("{id:guid}/deactivate")]
    [Authorize(Policy = "CompanyAdminOnly")]
    public async Task<IActionResult> DeactivateBranch(Guid id)
    {
        return (await deactivate.ExecuteAsync(id))
            .Match(NoContent);
    }
}