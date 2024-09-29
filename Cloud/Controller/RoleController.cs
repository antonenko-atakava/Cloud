using Cloud.Auth;
using Cloud.Domain.Http.Request.Role;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cloud.Controller;

public class RoleController : MainController
{
    [HttpGet]
    [Authorize(Policy = Policies.GET_ROLE)]
    public async Task<IActionResult> Get([FromQuery] GetRoleRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var role = await RoleService.Get(request.Id);
        return Ok(role);
    }

    [HttpPost]
    [Authorize(Policy = Policies.CREATE_ROLE)]
    public async Task<IActionResult> Create([FromBody] CreateRoleRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var role = await RoleService.Create(request);
        return Ok(role);
    }

    [HttpDelete]
    [Authorize(Policy = Policies.DELETE_ROLE)]
    public async Task<IActionResult> Delete([FromQuery] DeleteRoleRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var role = await RoleService.Delete(request);
        return Ok(role);
    }
}