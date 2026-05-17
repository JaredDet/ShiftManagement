using Microsoft.AspNetCore.Mvc;
using ShiftManagement.Api.Modules.Staff.Application.Collaborators;
using ShiftManagement.Api.Modules.Staff.Api.Contracts.Collaborators;

namespace ShiftManagement.Api.Modules.Staff.Api.Controllers;

[ApiController]
[Route("api/employees")]
public class EmployeeController(
    CreateCollaboratorUseCase create,
    GetCollaboratorUseCase get,
    ListCollaboratorsUseCase list,
    DeactivateCollaboratorUseCase deactivate) : ControllerBase
{
    private readonly CreateCollaboratorUseCase _create = create;
    private readonly GetCollaboratorUseCase _get = get;
    private readonly ListCollaboratorsUseCase _list = list;
    private readonly DeactivateCollaboratorUseCase _deactivate = deactivate;

    [HttpPost]
    public async Task<IActionResult> CreateEmployee(
        [FromBody] CreateCollaboratorRequest request
    )
    {
        var result = await _create.Execute(request);

        if (result.IsSuccess)
        {
            return CreatedAtAction(
                nameof(GetEmployee),
                new
                {
                    companyId = request.CompanyId,
                    id = result.Value!.Id
                },
                result.Value
            );
        }

        return result.Error.Code switch
        {
            "identity.user.not_found" => NotFound(result.Error),

            "organization.company.not_found" => NotFound(result.Error),

            "staff.employee.already_exists" => Conflict(result.Error),

            "staff.employee.user_not_eligible" => Conflict(result.Error),

            _ => BadRequest(result.Error)
        };
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetEmployee(
        [FromQuery] Guid companyId,
        Guid id
    )
    {
        var result = await _get.Execute(companyId, id);

        if (result.IsSuccess)
            return Ok(result.Value);

        return result.Error.Code switch
        {
            "staff.employee.not_found" => NotFound(result.Error),
            _ => BadRequest(result.Error)
        };
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

        if (result.IsSuccess)
            return NoContent();

        return result.Error.Code switch
        {
            "staff.employee.not_found" => NotFound(result.Error),
            _ => BadRequest(result.Error)
        };
    }
}