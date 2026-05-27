using Microsoft.AspNetCore.Mvc;
using ShiftManagement.Api.Modules.Staff.Application.Positions;
using ShiftManagement.Api.Modules.Staff.Api.Contracts.Positions;
using Microsoft.AspNetCore.Authorization;
using ShiftManagement.Api.Shared;

namespace ShiftManagement.Api.Modules.Staff.Api.Controllers;

[Authorize]
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
    [Authorize(Policy = "CompanyAdminOnly")]
    public async Task<IActionResult> CreatePosition(
        [FromBody] CreatePositionRequest request
    )
    {
        return (await create.Execute(request))
            .Match(value =>
                CreatedAtAction(
                    nameof(GetPosition),
                    new { id = value.Id },
                    value
                )
            );
    }

    [HttpPut("{id:guid}")]
    [Authorize(Policy = "CompanyAdminOnly")]
    public async Task<IActionResult> UpdatePosition(
        Guid id,
        [FromBody] UpdatePositionRequest request
    )
    {
        return (await update.Execute(id, request))
            .Match(Ok);
    }

    [HttpGet("{id:guid}")]
    [Authorize(Policy = "StaffReadAccess")]
    public async Task<IActionResult> GetPosition(Guid id)
    {
        return (await get.Execute(id))
            .Match(Ok);
    }

    [HttpGet]
    [Authorize(Policy = "StaffReadAccess")]
    public async Task<IActionResult> ListPositions()
    {
        return (await list.Execute())
            .Match(Ok);
    }

    [HttpDelete("{id:guid}")]
    [Authorize(Policy = "CompanyAdminOnly")]
    public async Task<IActionResult> DeactivatePosition(Guid id)
    {
        return (await deactivate.Execute(id))
            .Match(NoContent);
    }
}