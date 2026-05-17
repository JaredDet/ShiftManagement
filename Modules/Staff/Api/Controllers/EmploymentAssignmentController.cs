using Microsoft.AspNetCore.Mvc;
using ShiftManagement.Api.Modules.Staff.Api.Contracts.BranchAssignments;
using ShiftManagement.Api.Modules.Staff.Api.Contracts.PositionAssignments;
using ShiftManagement.Api.Modules.Staff.Api.Contracts.MainChanges;
using ShiftManagement.Api.Modules.Staff.Application.EmploymentAssignments;

namespace ShiftManagement.Api.Modules.Staff.Api.Controllers;

[ApiController]
[Route("api/employment-assignments")]
public class EmploymentAssignmentController : ControllerBase
{
    private readonly AssignBranchToCollaboratorUseCase _assignBranch;
    private readonly RemoveBranchFromCollaboratorUseCase _removeBranch;
    private readonly AssignPositionToCollaboratorUseCase _assignPosition;
    private readonly RemovePositionFromCollaboratorUseCase _removePosition;
    private readonly ChangeMainBranchUseCase _changeMainBranch;
    private readonly ChangeMainPositionUseCase _changeMainPosition;

    public EmploymentAssignmentController(
        AssignBranchToCollaboratorUseCase assignBranch,
        RemoveBranchFromCollaboratorUseCase removeBranch,
        AssignPositionToCollaboratorUseCase assignPosition,
        RemovePositionFromCollaboratorUseCase removePosition,
        ChangeMainBranchUseCase changeMainBranch,
        ChangeMainPositionUseCase changeMainPosition)
    {
        _assignBranch = assignBranch;
        _removeBranch = removeBranch;
        _assignPosition = assignPosition;
        _removePosition = removePosition;
        _changeMainBranch = changeMainBranch;
        _changeMainPosition = changeMainPosition;
    }

    [HttpPost("{employeeId:guid}/assign-branch")]
    public async Task<IActionResult> AssignBranch(
        Guid employeeId,
        [FromBody] AssignBranchToCollaboratorRequest request)
    {
        var result = await _assignBranch.Execute(employeeId, request);

        if (result.IsSuccess)
            return Ok();

        return result.Error.Code switch
        {
            "staff.employee.not_found" => NotFound(result.Error),
            "organization.branch.not_found" => NotFound(result.Error),

            "staff.assignment.already_exists" => Conflict(result.Error),

            _ => BadRequest(result.Error)
        };
    }

    [HttpPost("{employeeId:guid}/remove-branch")]
    public async Task<IActionResult> RemoveBranch(
        Guid employeeId,
        [FromBody] RemoveBranchFromCollaboratorRequest request)
    {
        var result = await _removeBranch.Execute(employeeId, request);

        if (result.IsSuccess)
            return Ok();

        return result.Error.Code switch
        {
            "staff.employee.not_found" => NotFound(result.Error),
            "staff.assignment.not_found" => NotFound(result.Error),

            _ => BadRequest(result.Error)
        };
    }

    [HttpPost("{employeeId:guid}/assign-position")]
    public async Task<IActionResult> AssignPosition(
        Guid employeeId,
        [FromBody] AssignPositionToCollaboratorRequest request)
    {
        var result = await _assignPosition.Execute(employeeId, request);

        if (result.IsSuccess)
            return Ok();

        return result.Error.Code switch
        {
            "staff.employee.not_found" => NotFound(result.Error),
            "staff.position.not_found" => NotFound(result.Error),

            "staff.assignment.already_exists" => Conflict(result.Error),

            _ => BadRequest(result.Error)
        };
    }

    [HttpPost("{employeeId:guid}/remove-position")]
    public async Task<IActionResult> RemovePosition(
        Guid employeeId,
        [FromBody] RemovePositionFromCollaboratorRequest request)
    {
        var result = await _removePosition.Execute(employeeId, request);

        if (result.IsSuccess)
            return Ok();

        return result.Error.Code switch
        {
            "staff.employee.not_found" => NotFound(result.Error),
            "staff.assignment.not_found" => NotFound(result.Error),

            _ => BadRequest(result.Error)
        };
    }

    [HttpPost("{employeeId:guid}/change-primary-branch")]
    public async Task<IActionResult> ChangePrimaryBranch(
        Guid employeeId,
        [FromBody] ChangeMainBranchRequest request)
    {
        var result = await _changeMainBranch.Execute(employeeId, request);

        if (result.IsSuccess)
            return Ok(result.Value);

        return result.Error.Code switch
        {
            "staff.employee.not_found" => NotFound(result.Error),
            "staff.assignment.not_found" => NotFound(result.Error),

            _ => BadRequest(result.Error)
        };
    }

    [HttpPost("{employeeId:guid}/change-primary-position")]
    public async Task<IActionResult> ChangePrimaryPosition(
        Guid employeeId,
        [FromBody] ChangeMainPositionRequest request)
    {
        var result = await _changeMainPosition.Execute(employeeId, request);

        if (result.IsSuccess)
            return Ok(result.Value);

        return result.Error.Code switch
        {
            "staff.employee.not_found" => NotFound(result.Error),
            "staff.assignment.not_found" => NotFound(result.Error),

            _ => BadRequest(result.Error)
        };
    }
}