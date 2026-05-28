using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShiftManagement.Api.BuildingBlocks.Results;
using ShiftManagement.Api.Modules.Scheduling.Api.Contracts;
using ShiftManagement.Api.Modules.Scheduling.Application.Shifts;

namespace ShiftManagement.Api.Modules.Scheduling.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/shifts")]
public class ShiftController(
    CreateShiftUseCase create,
    UpdateShiftUseCase update,
    AssignCollaboratorToShiftUseCase assign,
    ReplaceCollaboratorInShiftUseCase replace
) : ControllerBase
{
    [HttpPost]
    [Authorize(Policy = "ShiftManagementAccess")]
    public async Task<IActionResult> Create(
        [FromBody] CreateShiftRequest request
    )
    {
        return (await create.ExecuteAsync(request))
            .Match(Ok);
    }

    [HttpPut("{id:guid}")]
    [Authorize(Policy = "ShiftManagementAccess")]
    public async Task<IActionResult> Update(
        Guid id,
        [FromBody] UpdateShiftRequest request
    )
    {
        return (await update.ExecuteAsync(id, request))
            .Match(Ok);
    }

    [HttpPost("assign")]
    [Authorize(Policy = "ShiftManagementAccess")]
    public async Task<IActionResult> Assign(
    [FromBody] AssignCollaboratorToShiftRequest request
)
    {
        return (await assign.ExecuteAsync(request))
            .Match(Ok);
    }

    [HttpPost("replace")]
    [Authorize(Policy = "ShiftManagementAccess")]
    public async Task<IActionResult> Replace(
        [FromBody] ReplaceCollaboratorInShiftRequest request
    )
    {
        return (await replace.ExecuteAsync(request))
            .Match(Ok);
    }
}