using Microsoft.AspNetCore.Mvc;
using ShiftManagement.Api.Modules.Staff.Application.Collaborators;
using ShiftManagement.Api.Modules.Staff.Api.Contracts.Collaborators;
using Microsoft.AspNetCore.Authorization;
using ShiftManagement.Api.BuildingBlocks.Results;

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
        return (await create.Execute(request))
            .Match(value =>
                CreatedAtAction(
                    nameof(GetEmployee),
                    new { id = value.Id },
                    value
                )
            );
    }

    [HttpGet("{id:guid}")]
    [Authorize(Policy = "StaffReadAccess")]
    public async Task<IActionResult> GetEmployee(
        [FromQuery] Guid companyId,
        Guid id
    )
    {
        return (await get.Execute(companyId, id))
            .Match(Ok);
    }

    [HttpGet]
    [Authorize(Policy = "StaffReadAccess")]
    public async Task<IActionResult> ListEmployees([FromQuery] Guid companyId)
    {
        return (await list.Execute(companyId))
            .Match(Ok);
    }

    [HttpDelete("{id:guid}")]
    [Authorize(Policy = "CompanyAdminOnly")]
    public async Task<IActionResult> DeactivateEmployee(Guid id)
    {
        return (await deactivate.Execute(id))
            .Match(NoContent);
    }
}