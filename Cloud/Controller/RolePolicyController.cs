using Cloud.Auth;
using Cloud.Domain.Http.Request.RolePolicy;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cloud.Controller;

public class RolePolicyController : MainController
{
    [HttpPost]
    [Authorize(Policy = Policies.ADD_POLICY_TO_ROLE)]
    public async Task<IActionResult> AddRolePolicy([FromBody] CreateRolePolicyRequest rolePolicy)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        await RolePolicyService.Create(rolePolicy);
        return Ok("Политики успешно добавлены к роли");
    }
 
    [HttpDelete]
    [Authorize(Policy = Policies.REMOVE_POLICY_OF_ROLE)]
    public async Task<IActionResult> RemoveRolePolicy([FromQuery] DeleteRolePolicyRequest rolePolicy)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        await RolePolicyService.Delete(rolePolicy);
        return Ok("Политики успешно удалены у роли");
    }
}