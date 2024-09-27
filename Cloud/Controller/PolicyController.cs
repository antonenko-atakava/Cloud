using Cloud.Auth;
using Cloud.Domain.Entity;
using Cloud.Domain.Http.Request.Policy;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cloud.Controller;

public class PolicyController : MainController
{
    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetPolicy([FromQuery] GetPolicyRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await PolicyService.Get(request.Id);

        return Ok(result);
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetPolicyByName([FromQuery] GetByNamePolicyRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await PolicyService.GetByName(request.Name);

        return Ok(result);
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> SelectAll()
    {
        var result = await PolicyService.SelectAll();
        return Ok(result);
    }

    [HttpPost]
    [Authorize(Policy = Policies.USERS_ADD_POLICY)]
    public async Task<IActionResult> CreatePolicy([FromBody] CreatePolicyRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await PolicyService.Create(request);
        return Ok(result);
    }

    [HttpDelete]
    [Authorize(Policy = Policies.USERS_REMOVE_POLICY)]
    public async Task<IActionResult> DeletePolicy([FromQuery] DeletePolicyRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await PolicyService.Delete(request.Name);
        return Ok(result);
    }
}