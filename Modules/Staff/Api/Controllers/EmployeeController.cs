using Microsoft.AspNetCore.Mvc;
using ShiftManagement.Api.Modules.Staff.Application.Collaborators;
using ShiftManagement.Api.Modules.Staff.Api.Contracts.Collaborators;

namespace ShiftManagement.Api.Modules.Staff.Api.Controllers;

[ApiController]
[Route("api/employees")]
public class EmployeeController : ControllerBase
{
    private readonly CreateCollaboratorUseCase _create;
    private readonly GetCollaboratorUseCase _get;
    private readonly ListCollaboratorsUseCase _list;
    private readonly DeactivateCollaboratorUseCase _deactivate;

    public EmployeeController(
        CreateCollaboratorUseCase create,
        GetCollaboratorUseCase get,
        ListCollaboratorsUseCase list,
        DeactivateCollaboratorUseCase deactivate)
    {
        _create = create;
        _get = get;
        _list = list;
        _deactivate = deactivate;
    }

    [HttpPost]
    public async Task<IActionResult> CreateEmployee([FromBody] CreateCollaboratorRequest request)
    {
        var result = await _create.Execute(request);

        if (!result.IsSuccess)
            return BadRequest(result.Error);

        return CreatedAtAction(
            nameof(GetEmployee),
            new { id = result.Value!.Id },
            result.Value
        );
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetEmployee(Guid companyId, Guid id)
    {
        var result = await _get.Execute(companyId, id);

        if (!result.IsSuccess)
            return NotFound(result.Error);

        return Ok(result.Value);
    }

    [HttpGet]
    public async Task<IActionResult> ListEmployees([FromQuery] Guid companyId)
    {
        var result = await _list.Execute(companyId);

        if (!result.IsSuccess)
            return BadRequest(result.Error);

        return Ok(result.Value);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeactivateEmployee(Guid id)
    {
        var result = await _deactivate.Execute(id);

        if (!result.IsSuccess)
            return NotFound(result.Error);

        return NoContent();
    }
}