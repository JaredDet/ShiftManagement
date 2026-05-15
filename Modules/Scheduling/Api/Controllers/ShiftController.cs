using Microsoft.AspNetCore.Mvc;
using ShiftManagement.Api.Modules.Scheduling.Api.Contracts;
using ShiftManagement.Api.Modules.Scheduling.Application.Shifts;

namespace ShiftManagement.Api.Modules.Scheduling.Api.Controllers;

[ApiController]
[Route("api/shifts")]
public class ShiftController(
    CreateShiftUseCase createShiftUseCase,
    UpdateShiftUseCase updateShiftUseCase,
    AssignCollaboratorToShiftUseCase assignCollaboratorToShiftUseCase,
    ReplaceCollaboratorInShiftUseCase replaceCollaboratorInShiftUseCase
) : ControllerBase
{
    private readonly CreateShiftUseCase _createShiftUseCase = createShiftUseCase;

    [HttpPost]
    public async Task<IActionResult> Create(CreateShiftRequest request)
    {
        var result = await _createShiftUseCase.ExecuteAsync(request);

        if (!result.IsSuccess)
            return BadRequest(result.Error);

        return Ok(result.Value);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, UpdateShiftRequest request)
    {
        var result = await updateShiftUseCase.ExecuteAsync(id, request);

        if (!result.IsSuccess)
            return BadRequest(result.Error);

        return Ok(result.Value);
    }

    [HttpPost("assign")]
    public async Task<IActionResult> Assign(AssignCollaboratorToShiftRequest request)
    {
        var result = await assignCollaboratorToShiftUseCase.ExecuteAsync(request);

        if (!result.IsSuccess)
            return BadRequest(result.Error);

        return Ok(result.Value);
    }

    [HttpPost("replace")]
    public async Task<IActionResult> Replace(ReplaceCollaboratorInShiftRequest request)
    {
        var result = await replaceCollaboratorInShiftUseCase.ExecuteAsync(request);

        if (!result.IsSuccess)
            return BadRequest(result.Error);

        return Ok(result.Value);
    }
}