using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShiftManagement.Api.BuildingBlocks.Results;
using ShiftManagement.Api.Modules.Organization.Application.Companies;

namespace ShiftManagement.Api.Modules.Organization.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/companies")]
public class CompanyController(
    ListCompaniesUseCase list,
    GetCompanyUseCase get
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
}