using Cloud.Auth;
using Cloud.Domain.Http.Request.UserPolicy;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cloud.Controller;

public class UserPolicyController : MainController
{
    [HttpPost]
    [Authorize(Policy = Policies.USERS_ADD_POLICY)]
    public async Task<IActionResult> AddUserPolicy([FromBody] CreateUserPolicyRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        await UserPolicyService.Create(request);

        return Ok("Вы успешно добавили роль пользователю");
    }

    [HttpPost]
    [Authorize(Policy = Policies.USERS_REMOVE_POLICY)]
    public async Task<IActionResult> RemoveUserPolicy([FromBody] DeleteUserPolicyRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        await UserPolicyService.Delete(request);

        return Ok("Вы успешно удалили роль пользователю");
    }
}