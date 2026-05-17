using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShiftManagement.Api.Modules.Scheduling.Api.Contracts;
using ShiftManagement.Api.Modules.Scheduling.Application.Shifts;

namespace ShiftManagement.Api.Modules.Scheduling.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/shifts")]
public class ShiftController(
    CreateShiftUseCase createShiftUseCase,
    UpdateShiftUseCase updateShiftUseCase,
    AssignCollaboratorToShiftUseCase assignCollaboratorToShiftUseCase,
    ReplaceCollaboratorInShiftUseCase replaceCollaboratorInShiftUseCase
) : ControllerBase
{
    [HttpPost]
    [Authorize(Policy = "ShiftManagementAccess")]
    public async Task<IActionResult> Create(
        [FromBody] CreateShiftRequest request
    )
    {
        var result = await createShiftUseCase.ExecuteAsync(request);

        if (result.IsSuccess)
            return Ok(result.Value);

        return result.Error.Code switch
        {
            "organization.branch.not_found" => NotFound(result.Error),
            "staff.position.not_found" => NotFound(result.Error),
            _ => BadRequest(result.Error)
        };
    }

    [HttpPut("{id:guid}")]
    [Authorize(Policy = "ShiftManagementAccess")]
    public async Task<IActionResult> Update(
        Guid id,
        [FromBody] UpdateShiftRequest request
    )
    {
        var result = await updateShiftUseCase.ExecuteAsync(id, request);

        if (result.IsSuccess)
            return Ok(result.Value);

        return result.Error.Code switch
        {
            "scheduling.shift.not_found" => NotFound(result.Error),
            "organization.branch.not_found" => NotFound(result.Error),
            "staff.position.not_found" => NotFound(result.Error),
            _ => BadRequest(result.Error)
        };
    }

    [HttpPost("assign")]
    [Authorize(Policy = "ShiftManagementAccess")]
    public async Task<IActionResult> Assign(
    [FromBody] AssignCollaboratorToShiftRequest request
)
    {
        var result = await assignCollaboratorToShiftUseCase.ExecuteAsync(request);

        if (result.IsSuccess)
            return Ok(result.Value);

        return result.Error.Code switch
        {
            "scheduling.shift.not_found" => NotFound(result.Error),
            "scheduling.shift.already_assigned" => Conflict(result.Error),
            "scheduling.shift.invalid_state" => BadRequest(result.Error),
            _ => BadRequest(result.Error)
        };
    }

    [HttpPost("replace")]
    [Authorize(Policy = "ShiftManagementAccess")]
    public async Task<IActionResult> Replace(
        [FromBody] ReplaceCollaboratorInShiftRequest request
    )
    {
        var result = await replaceCollaboratorInShiftUseCase.ExecuteAsync(request);

        if (result.IsSuccess)
            return Ok(result.Value);

        return result.Error.Code switch
        {
            "scheduling.shift.not_found" => NotFound(result.Error),
            "scheduling.assignment.not_found" => NotFound(result.Error),
            "scheduling.shift.already_assigned" => Conflict(result.Error),
            "scheduling.shift.invalid_state" => BadRequest(result.Error),
            _ => BadRequest(result.Error)
        };
    }
}