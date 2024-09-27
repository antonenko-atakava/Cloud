using Microsoft.AspNetCore.Mvc;

namespace Cloud.Controller;

public class AuthController : MainController
{
    [HttpGet]
    public IActionResult Forbidden()
    {
        return BadRequest("Access denied");
    }
}