using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShiftManagement.Api.Modules.Organization.Api.Contracts.Companies;
using ShiftManagement.Api.Modules.Organization.Application.Companies;

namespace ShiftManagement.Api.Modules.Organization.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/companies")]
public class CompanyController(
    CreateCompanyUseCase createCompanyUseCase,
    ListCompaniesUseCase listCompaniesUseCase,
    GetCompanyUseCase getCompanyUseCase,
    UpdateCompanyUseCase updateCompanyUseCase,
    DeactivateCompanyUseCase deactivateCompanyUseCase
) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> ListCompanies()
    {
        var result = await listCompaniesUseCase.ExecuteAsync();

        if (!result.IsSuccess)
            return BadRequest(result.Error);

        return Ok(result.Value);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetCompany(Guid id)
    {
        var result = await getCompanyUseCase.ExecuteAsync(id);

        if (result.IsSuccess)
            return Ok(result.Value);

        return result.Error.Code switch
        {
            "organization.company.not_found" => NotFound(result.Error),
            _ => BadRequest(result.Error)
        };
    }

    [HttpPost]
    [Authorize(Policy = "CompanyAdminOnly")]
    public async Task<IActionResult> CreateCompany(
    CreateCompanyRequest request
    )
    {
        var result = await createCompanyUseCase.ExecuteAsync(request);

        if (result.IsSuccess)
            return CreatedAtAction(
                nameof(GetCompany),
                new { id = result.Value!.Id },
                result.Value
            );

        return result.Error.Code switch
        {
            "organization.company.already_exists" => Conflict(result.Error),
            _ => BadRequest(result.Error)
        };
    }

    [HttpPut("{id:guid}")]
    [Authorize(Policy = "CompanyAdminOnly")]
    public async Task<IActionResult> UpdateCompany(
       Guid id,
       UpdateCompanyRequest request
   )
    {
        var result = await updateCompanyUseCase.ExecuteAsync(id, request);

        if (result.IsSuccess)
            return Ok(result.Value);

        return result.Error.Code switch
        {
            "organization.company.not_found" => NotFound(result.Error),
            "organization.company.already_exists" => Conflict(result.Error),
            _ => BadRequest(result.Error)
        };
    }

    [HttpPatch("{id:guid}/deactivate")]
    [Authorize(Policy = "CompanyAdminOnly")]
    public async Task<IActionResult> DeactivateCompany(Guid id)
    {
        var result = await deactivateCompanyUseCase.ExecuteAsync(id);

        if (result.IsSuccess)
            return NoContent();

        return result.Error.Code switch
        {
            "organization.company.not_found" => NotFound(result.Error),
            _ => BadRequest(result.Error)
        };
    }
}