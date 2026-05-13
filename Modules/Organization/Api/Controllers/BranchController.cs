using Microsoft.AspNetCore.Mvc;
using ShiftManagement.Api.Modules.Organization.Api.Contracts;
using ShiftManagement.Api.Modules.Organization.Application.UseCases;

namespace ShiftManagement.Api.Modules.Organization.Api.Controllers;

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

        if (!result.IsSuccess)
        {
            return NotFound(result.Error);
        }

        return Ok(result.Value);
    }

    [HttpGet]
    [HttpGet]
    public async Task<IActionResult> ListBranches([FromQuery] Guid companyId)
    {
        var result = await listBranchesUseCase.ExecuteAsync(companyId);

        if (!result.IsSuccess)
            return NotFound(result.Error);

        return Ok(result.Value);
    }

    [HttpPost]
    public async Task<IActionResult> CreateBranch(
        CreateBranchRequest request
    )
    {
        var result = await createBranchUseCase
            .ExecuteAsync(request);

        if (!result.IsSuccess)
        {
            return BadRequest(result.Error);
        }

        return CreatedAtAction(
            nameof(GetBranch),
            new { id = result.Value!.Id },
            result.Value
        );
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateBranch(
        Guid id,
        UpdateBranchRequest request
    )
    {
        var result = await updateBranchUseCase
            .ExecuteAsync(id, request);

        if (!result.IsSuccess)
        {
            return NotFound(result.Error);
        }

        return Ok(result.Value);
    }

    [HttpPatch("{id:guid}/deactivate")]
    public async Task<IActionResult> DeactivateBranch(Guid id)
    {
        var result = await deactivateBranchUseCase
            .ExecuteAsync(id);

        if (!result.IsSuccess)
        {
            return NotFound(result.Error);
        }

        return Ok(result.Value);
    }
}