using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShiftManagement.Api.Modules.Organization.Api.Contracts.Branches;
using ShiftManagement.Api.Modules.Organization.Application.Branches;

namespace ShiftManagement.Api.Modules.Organization.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/branches")]
public class BranchController(
    CreateBranchUseCase createBranchUseCase,
    GetBranchUseCase getBranchUseCase,
    UpdateBranchUseCase updateBranchUseCase,
    DeactivateBranchUseCase deactivateBranchUseCase,
    ListBranchesUseCase listBranchesUseCase
) : ControllerBase
{
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetBranch(Guid id)
    {
        var result = await getBranchUseCase.ExecuteAsync(id);

        if (result.IsSuccess)
            return Ok(result.Value);

        return result.Error.Code switch
        {
            "organization.branch.not_found" => NotFound(result.Error),
            _ => BadRequest(result.Error)
        };
    }

    [HttpGet]
    public async Task<IActionResult> ListBranches([FromQuery] Guid companyId)
    {
        var result = await listBranchesUseCase.ExecuteAsync(companyId);

        if (result.IsSuccess)
            return Ok(result.Value);

        return result.Error.Code switch
        {
            "organization.company.not_found" => NotFound(result.Error),
            _ => BadRequest(result.Error)
        };
    }

    [HttpPost]
    [Authorize(Policy = "CompanyAdminOnly")]
    public async Task<IActionResult> CreateBranch(
            CreateBranchRequest request
        )
    {
        var result = await createBranchUseCase.ExecuteAsync(request);

        if (result.IsSuccess)
            return CreatedAtAction(
                nameof(GetBranch),
                new { id = result.Value!.Id },
                result.Value
            );

        return result.Error.Code switch
        {
            "organization.company.not_found" => NotFound(result.Error),
            "organization.branch.already_exists" => Conflict(result.Error),
            _ => BadRequest(result.Error)
        };
    }

    [HttpPut("{id:guid}")]
    [Authorize(Policy = "CompanyAdminOnly")]
    public async Task<IActionResult> UpdateBranch(
        Guid id,
        UpdateBranchRequest request
    )
    {
        var result = await updateBranchUseCase.ExecuteAsync(id, request);

        if (result.IsSuccess)
            return Ok(result.Value);

        return result.Error.Code switch
        {
            "organization.branch.not_found" => NotFound(result.Error),
            "organization.branch.already_exists" => Conflict(result.Error),
            _ => BadRequest(result.Error)
        };
    }

    [HttpPatch("{id:guid}/deactivate")]
    [Authorize(Policy = "CompanyAdminOnly")]
    public async Task<IActionResult> DeactivateBranch(Guid id)
    {
        var result = await deactivateBranchUseCase.ExecuteAsync(id);

        if (result.IsSuccess)
            return NoContent();

        return result.Error.Code switch
        {
            "organization.branch.not_found" => NotFound(result.Error),
            _ => BadRequest(result.Error)
        };
    }
}