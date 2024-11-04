using System.Security.Claims;
using Cloud.Auth;
using Cloud.Domain.Http.Request.User;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cloud.Controller;

public class AuthController : MainController
{
    [HttpGet]
    public IActionResult Forbidden()
    {
        return BadRequest("Access denied");
    }

    [HttpPost]
    public async Task<IActionResult> LogIn([FromBody] LoginUserRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var userId = await UserService.Login(request.Login, request.Password);

        var claim = new[]
        {
            new Claim(CustomClaimTypes.Identifier, userId.ToString()),
        };

        var identity = new ClaimsIdentity(claim, CookieAuthenticationDefaults.AuthenticationScheme);
        var principal = new ClaimsPrincipal(identity);

        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

        return Ok("Вы усешно вошли в аккаунт");
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetUserInfo()
    {
        
        var identity = User.Identities.FirstOrDefault(i =>
            i.AuthenticationType == CookieAuthenticationDefaults.AuthenticationScheme);

        if (identity == null)
            return BadRequest("Вы не вошли в аккаунт");

        var claim = identity.FindFirst(CustomClaimTypes.Identifier);
        var userId = Guid.Parse(claim?.Value ?? throw new InvalidCastException());

        var user = await UserService.Get(userId);

        return Ok(user);
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return Ok("Вы вышли из аккаунта");
    }
}