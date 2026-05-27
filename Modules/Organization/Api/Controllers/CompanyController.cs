using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShiftManagement.Api.Modules.Organization.Api.Contracts.Companies;
using ShiftManagement.Api.Modules.Organization.Application.Companies;
using ShiftManagement.Api.Shared;

namespace ShiftManagement.Api.Modules.Organization.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/companies")]
public class CompanyController(
    CreateCompanyUseCase create,
    ListCompaniesUseCase list,
    GetCompanyUseCase get,
    UpdateCompanyUseCase update,
    DeactivateCompanyUseCase deactivate
) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> ListCompanies()
    {
        return (await list.ExecuteAsync())
            .Match(Ok);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetCompany(Guid id)
    {
        return (await get.ExecuteAsync(id))
            .Match(Ok);
    }

    [HttpPost]
    [Authorize(Policy = "CompanyAdminOnly")]
    public async Task<IActionResult> CreateCompany(
    CreateCompanyRequest request
    )
    {
        return (await create.ExecuteAsync(request))
            .Match(value =>
                CreatedAtAction(
                    nameof(GetCompany),
                    new { id = value.Id },
                    value
                )
            );
    }

    [HttpPut("{id:guid}")]
    [Authorize(Policy = "CompanyAdminOnly")]
    public async Task<IActionResult> UpdateCompany(
       Guid id,
       UpdateCompanyRequest request
   )
    {
        return (await update.ExecuteAsync(id, request))
            .Match(Ok);
    }

    [HttpPatch("{id:guid}/deactivate")]
    [Authorize(Policy = "CompanyAdminOnly")]
    public async Task<IActionResult> DeactivateCompany(Guid id)
    {
        return (await deactivate.ExecuteAsync(id))
            .Match(NoContent);
    }
}