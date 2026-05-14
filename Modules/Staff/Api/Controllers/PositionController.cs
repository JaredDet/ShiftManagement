using Microsoft.AspNetCore.Mvc;
using ShiftManagement.Api.Modules.Staff.Application.Positions;
using ShiftManagement.Api.Modules.Staff.Api.Contracts.Positions;

namespace ShiftManagement.Api.Modules.Staff.Api.Controllers;

[ApiController]
[Route("api/positions")]
public class PositionController : ControllerBase
{
    private readonly CreatePositionUseCase _create;
    private readonly UpdatePositionUseCase _update;
    private readonly GetPositionUseCase _get;
    private readonly ListPositionsUseCase _list;
    private readonly DeactivatePositionUseCase _deactivate;

    public PositionController(
        CreatePositionUseCase create,
        UpdatePositionUseCase update,
        GetPositionUseCase get,
        ListPositionsUseCase list,
        DeactivatePositionUseCase deactivate)
    {
        _create = create;
        _update = update;
        _get = get;
        _list = list;
        _deactivate = deactivate;
    }

    [HttpPost]
    public async Task<IActionResult> CreatePosition([FromBody] CreatePositionRequest request)
    {
        var result = await _create.Execute(request);

        if (!result.IsSuccess)
            return BadRequest(result.Error);

        return CreatedAtAction(
            nameof(GetPosition),
            new { id = result.Value!.Id },
            result.Value
        );
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdatePosition(Guid id, [FromBody] UpdatePositionRequest request)
    {
        var result = await _update.Execute(id, request);

        if (!result.IsSuccess)
            return BadRequest(result.Error);

        return Ok(result.Value);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetPosition(Guid id)
    {
        var result = await _get.Execute(id);

        if (!result.IsSuccess)
            return NotFound(result.Error);

        return Ok(result.Value);
    }

    [HttpGet]
    public async Task<IActionResult> ListPositions()
    {
        var result = await _list.Execute();

        if (!result.IsSuccess)
            return BadRequest(result.Error);

        return Ok(result.Value);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeactivatePosition(Guid id)
    {
        var result = await _deactivate.Execute(id);

        if (!result.IsSuccess)
            return NotFound(result.Error);

        return NoContent();
    }
}