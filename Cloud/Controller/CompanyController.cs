using Cloud.Domain.Http.Request;
using Cloud.Domain.Http.Request.Company;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cloud.Controller;

public class CompanyController : MainController
{
    [HttpGet]
    [Authorize]
    public async Task<IActionResult> Get([FromQuery] GetCompanyRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var company = await CompanyService.Get(request.Id);
        return Ok(company);
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetByName([FromQuery] GetByNameCompanyRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var company = await CompanyService.GetByName(request.Name);
        return Ok(company);
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> SelectAll()
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var companies = await CompanyService.SelectAll();
        return Ok(companies);
    }
    
    [HttpGet]
    [Authorize]
    public async Task<IActionResult> Pagination([FromQuery] PaginationRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var companies = await UserService.Pagination(request.Number, request.Size);
        return Ok(companies);
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Create([FromBody] CreateCompanyRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var company = await CompanyService.Create(request);
        return Ok(company);
    }

    [HttpPut]
    [Authorize]
    public async Task<IActionResult> Update([FromBody] UpdateCompanyRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var company = await CompanyService.Update(request);
        return Ok(company);
    }

    [HttpDelete]
    [Authorize]
    public async Task<IActionResult> Delete([FromQuery] DeleteCompanyRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var company = await CompanyService.Delete(request);
        return Ok(company);
    }
}