using Microsoft.AspNetCore.Mvc;
using ShiftManagement.Api.Modules.Staff.Api.Contracts.BranchAssignments;
using ShiftManagement.Api.Modules.Staff.Api.Contracts.PositionAssignments;
using ShiftManagement.Api.Modules.Staff.Api.Contracts.MainChanges;
using ShiftManagement.Api.Modules.Staff.Application.EmploymentAssignments;
using Microsoft.AspNetCore.Authorization;
using ShiftManagement.Api.BuildingBlocks.Results;

namespace ShiftManagement.Api.Modules.Staff.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/employment-assignments")]
public class EmploymentAssignmentController(
    AssignBranchToCollaboratorUseCase assignBranch,
    RemoveBranchFromCollaboratorUseCase removeBranch,
    AssignPositionToCollaboratorUseCase assignPosition,
    RemovePositionFromCollaboratorUseCase removePosition,
    ChangeMainBranchUseCase changeMainBranch,
    ChangeMainPositionUseCase changeMainPosition) : ControllerBase
{

    [HttpPost("{collaboratorId:guid}/assign-branch")]
    [Authorize(Policy = "CompanyAdminOnly")]
    public async Task<IActionResult> AssignBranch(
        Guid collaboratorId,
        [FromBody] AssignBranchToCollaboratorRequest request)
    {
        return (await assignBranch.Execute(collaboratorId, request))
            .Match(NoContent);
    }

    [HttpPost("{collaboratorId:guid}/remove-branch")]
    [Authorize(Policy = "CompanyAdminOnly")]
    public async Task<IActionResult> RemoveBranch(
        Guid collaboratorId,
        [FromBody] RemoveBranchFromCollaboratorRequest request)
    {
        return (await removeBranch.Execute(collaboratorId, request))
            .Match(NoContent);
    }

    [HttpPost("{collaboratorId:guid}/assign-position")]
    [Authorize(Policy = "CompanyAdminOnly")]
    public async Task<IActionResult> AssignPosition(
        Guid collaboratorId,
        [FromBody] AssignPositionToCollaboratorRequest request)
    {
        return (await assignPosition.Execute(collaboratorId, request))
            .Match(NoContent);
    }

    [HttpPost("{collaboratorId:guid}/remove-position")]
    [Authorize(Policy = "CompanyAdminOnly")]
    public async Task<IActionResult> RemovePosition(
        Guid collaboratorId,
        [FromBody] RemovePositionFromCollaboratorRequest request)
    {
        return (await removePosition.Execute(collaboratorId, request))
            .Match(NoContent);
    }

    [HttpPost("{collaboratorId:guid}/change-primary-branch")]
    [Authorize(Policy = "CompanyAdminOnly")]
    public async Task<IActionResult> ChangePrimaryBranch(
        Guid collaboratorId,
        [FromBody] ChangeMainBranchRequest request)
    {
        return (await changeMainBranch.Execute(collaboratorId, request))
            .Match(Ok);
    }

    [HttpPost("{collaboratorId:guid}/change-primary-position")]
    [Authorize(Policy = "CompanyAdminOnly")]
    public async Task<IActionResult> ChangePrimaryPosition(
        Guid collaboratorId,
        [FromBody] ChangeMainPositionRequest request)
    {
        return (await changeMainPosition.Execute(collaboratorId, request))
            .Match(Ok);
    }
}