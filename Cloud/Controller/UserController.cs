using System.Security.Claims;
using Cloud.Auth;
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
    
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateUserRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

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
}