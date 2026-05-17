using Microsoft.AspNetCore.Mvc;
using ShiftManagement.Api.Modules.Staff.Application.Positions;
using ShiftManagement.Api.Modules.Staff.Api.Contracts.Positions;

namespace ShiftManagement.Api.Modules.Staff.Api.Controllers;

[ApiController]
[Route("api/positions")]
public class PositionController(
    CreatePositionUseCase create,
    UpdatePositionUseCase update,
    GetPositionUseCase get,
    ListPositionsUseCase list,
    DeactivatePositionUseCase deactivate) : ControllerBase
{

    [HttpPost]
    public async Task<IActionResult> CreatePosition(
        [FromBody] CreatePositionRequest request
    )
    {
        var result = await create.Execute(request);

        if (result.IsSuccess)
        {
            return CreatedAtAction(
                nameof(GetPosition),
                new { id = result.Value!.Id },
                result.Value
            );
        }

        return result.Error.Code switch
        {
            "staff.position.already_exists" => Conflict(result.Error),
            _ => BadRequest(result.Error)
        };
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdatePosition(
        Guid id,
        [FromBody] UpdatePositionRequest request
    )
    {
        var result = await update.Execute(id, request);

        if (result.IsSuccess)
            return Ok(result.Value);

        return result.Error.Code switch
        {
            "staff.position.not_found" => NotFound(result.Error),
            "staff.position.already_exists" => Conflict(result.Error),
            _ => BadRequest(result.Error)
        };
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetPosition(Guid id)
    {
        var result = await get.Execute(id);

        if (result.IsSuccess)
            return Ok(result.Value);

        return result.Error.Code switch
        {
            "staff.position.not_found" => NotFound(result.Error),
            _ => BadRequest(result.Error)
        };
    }

    [HttpGet]
    public async Task<IActionResult> ListPositions()
    {
        var result = await list.Execute();

        if (!result.IsSuccess)
            return BadRequest(result.Error);

        return Ok(result.Value);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeactivatePosition(Guid id)
    {
        var result = await deactivate.Execute(id);

        if (result.IsSuccess)
            return NoContent();

        return result.Error.Code switch
        {
            "staff.position.not_found" => NotFound(result.Error),
            _ => BadRequest(result.Error)
        };
    }
}