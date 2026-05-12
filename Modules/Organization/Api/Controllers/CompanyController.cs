using Microsoft.AspNetCore.Mvc;
using ShiftManagement.Api.Modules.Organization.Api.Contracts;
using ShiftManagement.Api.Modules.Organization.Application.UseCases;

namespace ShiftManagement.Api.Modules.Organization.Api.Controllers;

[ApiController]
[Route("api/companies")]
public class CompanyController(
    CreateCompanyUseCase createCompanyUseCase,
    GetCompanyUseCase getCompanyUseCase,
    UpdateCompanyUseCase updateCompanyUseCase,
    DeactivateCompanyUseCase deactivateCompanyUseCase
) : ControllerBase
{
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetCompany(Guid id)
    {
        var result = await getCompanyUseCase.ExecuteAsync(id);

        if (!result.IsSuccess)
        {
            return NotFound(result.Error);
        }

        return Ok(result.Value);
    }

    [HttpPost]
    public async Task<IActionResult> CreateCompany(
        CreateCompanyRequest request
    )
    {
        var result = await createCompanyUseCase
            .ExecuteAsync(request);

        if (!result.IsSuccess)
        {
            return BadRequest(result.Error);
        }

        return CreatedAtAction(
            nameof(GetCompany),
            new { id = result.Value!.Id },
            result.Value
        );
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateCompany(
        Guid id,
        UpdateCompanyRequest request
    )
    {
        var result = await updateCompanyUseCase
            .ExecuteAsync(id, request);

        if (!result.IsSuccess)
        {
            return NotFound(result.Error);
        }

        return Ok(result.Value);
    }

    [HttpPatch("{id:guid}/deactivate")]
    public async Task<IActionResult> DeactivateCompany(Guid id)
    {
        var result = await deactivateCompanyUseCase
            .ExecuteAsync(id);

        if (!result.IsSuccess)
        {
            return NotFound(result.Error);
        }

        return Ok(result.Value);
    }
}