using System.Security.Claims;
using Cloud.Auth;
using Cloud.Domain.Http.Request;
using Cloud.Domain.Http.Request.User;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cloud.Controller;

public class UserController : MainController
{
    [HttpGet]
    [Authorize]
    public async Task<IActionResult> Get([FromQuery] GetUserRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var user = await UserService.Get(request.Id);

        return Ok(user);
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetByName([FromQuery] GetByNameUserRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var user = await UserService.GetByName(request.Name);

        return Ok(user);
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetByEmail([FromQuery] GetByEmailUserRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var user = await UserService.GetByEmail(request.Email);

        return Ok(user);
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetByPhone([FromQuery] GetByPhoneUserRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var user = await UserService.GetByPhone(request.Phone);

        return Ok(user);
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> SelectAll()
    {
        var users = await UserService.SelectAll();
        return Ok(users);
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> Pagination([FromQuery] PaginationRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var users = await UserService.Pagination(request.Number, request.Size);
        return Ok(users);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateUserRequest request)
    {
        var user = await UserService.Create(request);
        var claim = new[]
        {
            new Claim(CustomClaimTypes.Identifier, user.Id.ToString()),
        };

        var identity = new ClaimsIdentity(claim, CookieAuthenticationDefaults.AuthenticationScheme);
        var principal = new ClaimsPrincipal(identity);

        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

        return Ok(user);
    }

    [HttpPut]
    [Authorize]
    public async Task<IActionResult> Update([FromBody] UpdateUserRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var users = await UserService.Update(request);
        return Ok(users);
    }

    [HttpPut]
    [Authorize]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> UpdateAvatar([FromForm] UpdateAvatarUserRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var users = await UserService.UpdateAvatar(request);
        return Ok(users);
    }

    [HttpDelete]
    [Authorize]
    public async Task<IActionResult> Delete([FromQuery] DeleteUserRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        await UserService.Delete(request);
        return Ok("Аккаунт успешно удален");
    }
}