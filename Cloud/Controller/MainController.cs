using Cloud.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Cloud.Controller;

[ApiController]
[Route("api/[controller]/[action]")]
public abstract class MainController : ControllerBase
{
    protected IUserService UserService => 
        HttpContext.RequestServices.GetRequiredService<IUserService>();
    
    protected IPolicyService PolicyService => 
        HttpContext.RequestServices.GetRequiredService<IPolicyService>();
    
    protected IUserPolicyService UserPolicyService =>
        HttpContext.RequestServices.GetRequiredService<IUserPolicyService>();
}