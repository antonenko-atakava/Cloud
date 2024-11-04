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

    protected IRolePolicyService RolePolicyService =>
        HttpContext.RequestServices.GetRequiredService<IRolePolicyService>();

    protected IRoleService RoleService =>
        HttpContext.RequestServices.GetRequiredService<IRoleService>();

    protected IUserRoleService UserRoleService =>
        HttpContext.RequestServices.GetRequiredService<IUserRoleService>();

    protected ICompanyService CompanyService =>
        HttpContext.RequestServices.GetRequiredService<ICompanyService>();

    protected IDirectoryService DirectoryService
        => HttpContext.RequestServices.GetRequiredService<IDirectoryService>();
}