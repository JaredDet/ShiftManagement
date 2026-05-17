using Microsoft.AspNetCore.Mvc;
using ShiftManagement.Api.Modules.Staff.Application.Collaborators;
using ShiftManagement.Api.Modules.Staff.Api.Contracts.Collaborators;
using Microsoft.AspNetCore.Authorization;

namespace ShiftManagement.Api.Modules.Staff.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/employees")]
public class EmployeeController(
    CreateCollaboratorUseCase create,
    GetCollaboratorUseCase get,
    ListCollaboratorsUseCase list,
    DeactivateCollaboratorUseCase deactivate) : ControllerBase
{
    [HttpPost]
    [Authorize(Policy = "CompanyAdminOnly")]
    public async Task<IActionResult> CreateEmployee(
        [FromBody] CreateCollaboratorRequest request
    )
    {
        var result = await create.Execute(request);

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
    [Authorize(Policy = "StaffReadAccess")]
    public async Task<IActionResult> GetEmployee(
        [FromQuery] Guid companyId,
        Guid id
    )
    {
        var result = await get.Execute(companyId, id);

        if (result.IsSuccess)
            return Ok(result.Value);

        return result.Error.Code switch
        {
            "staff.employee.not_found" => NotFound(result.Error),
            _ => BadRequest(result.Error)
        };
    }

    [HttpGet]
    [Authorize(Policy = "StaffReadAccess")]
    public async Task<IActionResult> ListEmployees([FromQuery] Guid companyId)
    {
        var result = await list.Execute(companyId);

        if (!result.IsSuccess)
            return BadRequest(result.Error);

        return Ok(result.Value);
    }

    [HttpDelete("{id:guid}")]
    [Authorize(Policy = "CompanyAdminOnly")]
    public async Task<IActionResult> DeactivateEmployee(Guid id)
    {
        var result = await deactivate.Execute(id);

        if (result.IsSuccess)
            return NoContent();

        return result.Error.Code switch
        {
            "staff.employee.not_found" => NotFound(result.Error),
            _ => BadRequest(result.Error)
        };
    }
}