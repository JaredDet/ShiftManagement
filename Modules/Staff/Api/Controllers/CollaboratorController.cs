using Microsoft.AspNetCore.Mvc;
using ShiftManagement.Api.Modules.Staff.Application.Collaborators;
using ShiftManagement.Api.Modules.Staff.Api.Contracts.Collaborators;
using Microsoft.AspNetCore.Authorization;
using ShiftManagement.Api.BuildingBlocks.Results;

namespace ShiftManagement.Api.Modules.Staff.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/companies/{companyId:guid}/collaborators")]
public class CollaboratorController(
    CreateCollaboratorUseCase create,
    GetCollaboratorUseCase get,
    ListCollaboratorsUseCase list,
    DeactivateCollaboratorUseCase deactivate
) : ControllerBase
{
    [HttpPost]
    [Authorize(Policy = "CompanyAdminOnly")]
    public async Task<IActionResult> CreateCollaborator(
        Guid companyId,
        [FromBody] CreateCollaboratorRequest request
    )
    {
        request = request with { CompanyId = companyId };

        return (await create.Execute(request))
            .Match(id =>
                CreatedAtAction(
                    nameof(GetCollaborator),
                    new { companyId, id },
                    new { id }
                )
            );
    }

    [HttpGet("{id:guid}")]
    [Authorize(Policy = "StaffReadAccess")]
    public async Task<IActionResult> GetCollaborator(
        Guid companyId,
        Guid id
    )
    {
        return (await get.Execute(companyId, id))
            .Match(Ok);
    }

    [HttpGet]
    [Authorize(Policy = "StaffReadAccess")]
    public async Task<IActionResult> ListCollaborators(
        Guid companyId
    )
    {
        return (await list.Execute(companyId))
            .Match(Ok);
    }

    [HttpDelete("{id:guid}")]
    [Authorize(Policy = "CompanyAdminOnly")]
    public async Task<IActionResult> DeactivateCollaborator(
        Guid companyId,
        Guid id
    )
    {
        return (await deactivate.Execute(companyId, id))
            .Match(NoContent);
    }
}