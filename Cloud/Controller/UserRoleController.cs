using Cloud.Auth;
using Cloud.Domain.Http.Request.UserRole;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cloud.Controller;

public class UserRoleController : MainController
{
    [HttpPost]
    [Authorize(Policy = Policies.USERS_ADD_ROLE)]
    public async Task<IActionResult> AddUserRole([FromBody] CreateUserRoleRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        await UserRoleService.Create(request);
        return Ok("Роль успешно добавлена к пользователю");
    }

    [HttpDelete]
    [Authorize(Policy = Policies.USERS_REMOVE_ROLE)]
    public async Task<IActionResult> RemoveUserRole([FromQuery] DeleteUserRoleRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        await UserRoleService.Delete(request);
        return Ok("Роль успешно удалена у пользователя");
    }
}