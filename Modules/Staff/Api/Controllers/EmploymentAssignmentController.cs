using Microsoft.AspNetCore.Mvc;
using ShiftManagement.Api.Modules.Staff.Api.Contracts.BranchAssignments;
using ShiftManagement.Api.Modules.Staff.Api.Contracts.PositionAssignments;
using ShiftManagement.Api.Modules.Staff.Api.Contracts.MainChanges;
using ShiftManagement.Api.Modules.Staff.Application.EmploymentAssignments;
using Microsoft.AspNetCore.Authorization;
using ShiftManagement.Api.Shared;

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

    [HttpPost("{employeeId:guid}/assign-branch")]
    [Authorize(Policy = "CompanyAdminOnly")]
    public async Task<IActionResult> AssignBranch(
        Guid employeeId,
        [FromBody] AssignBranchToCollaboratorRequest request)
    {
        return (await assignBranch.Execute(employeeId, request))
            .Match(Ok);
    }

    [HttpPost("{employeeId:guid}/remove-branch")]
    [Authorize(Policy = "CompanyAdminOnly")]
    public async Task<IActionResult> RemoveBranch(
        Guid employeeId,
        [FromBody] RemoveBranchFromCollaboratorRequest request)
    {
        return (await removeBranch.Execute(employeeId, request))
            .Match(Ok);
    }

    [HttpPost("{employeeId:guid}/assign-position")]
    [Authorize(Policy = "CompanyAdminOnly")]
    public async Task<IActionResult> AssignPosition(
        Guid employeeId,
        [FromBody] AssignPositionToCollaboratorRequest request)
    {
        return (await assignPosition.Execute(employeeId, request))
            .Match(Ok);
    }

    [HttpPost("{employeeId:guid}/remove-position")]
    [Authorize(Policy = "CompanyAdminOnly")]
    public async Task<IActionResult> RemovePosition(
        Guid employeeId,
        [FromBody] RemovePositionFromCollaboratorRequest request)
    {
        return (await removePosition.Execute(employeeId, request))
            .Match(Ok);
    }

    [HttpPost("{employeeId:guid}/change-primary-branch")]
    [Authorize(Policy = "CompanyAdminOnly")]
    public async Task<IActionResult> ChangePrimaryBranch(
        Guid employeeId,
        [FromBody] ChangeMainBranchRequest request)
    {
        return (await changeMainBranch.Execute(employeeId, request))
            .Match(Ok);
    }

    [HttpPost("{employeeId:guid}/change-primary-position")]
    [Authorize(Policy = "CompanyAdminOnly")]
    public async Task<IActionResult> ChangePrimaryPosition(
        Guid employeeId,
        [FromBody] ChangeMainPositionRequest request)
    {
        return (await changeMainPosition.Execute(employeeId, request))
            .Match(Ok);
    }
}