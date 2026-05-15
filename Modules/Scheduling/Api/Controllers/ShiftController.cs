using Microsoft.AspNetCore.Mvc;
using ShiftManagement.Api.Modules.Scheduling.Api.Contracts;
using ShiftManagement.Api.Modules.Scheduling.Application;

namespace ShiftManagement.Api.Modules.Scheduling.Api.Controllers;

[ApiController]
[Route("api/shifts")]
public class ShiftController(CreateShiftUseCase createShiftUseCase, UpdateShiftUseCase updateShiftUseCase) : ControllerBase
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
    public Task<IActionResult> Assign(AssignCollaboratorToShiftRequest request)
    {
        throw new NotImplementedException();
    }

    [HttpPost("replace")]
    public Task<IActionResult> Replace(ReplaceCollaboratorInShiftRequest request)
    {
        throw new NotImplementedException();
    }
}