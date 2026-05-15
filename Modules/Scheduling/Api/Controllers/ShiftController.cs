using Microsoft.AspNetCore.Mvc;
using ShiftManagement.Api.Modules.Scheduling.Api.Contracts;
using ShiftManagement.Api.Modules.Scheduling.Application;

namespace ShiftManagement.Api.Modules.Scheduling.Api.Controllers;

[ApiController]
[Route("api/shifts")]
public class ShiftController : ControllerBase
{
    private readonly CreateShiftUseCase _createShiftUseCase;

    public ShiftController(CreateShiftUseCase createShiftUseCase)
    {
        _createShiftUseCase = createShiftUseCase;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateShiftRequest request)
    {
        var result = await _createShiftUseCase.ExecuteAsync(request);

        if (!result.IsSuccess)
            return BadRequest(result.Error);

        return Ok(result.Value);
    }

    [HttpPut("{id:guid}")]
    public Task<IActionResult> Update(Guid id, UpdateShiftRequest request)
    {
        throw new NotImplementedException();
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