using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;

namespace Cloud.Auth;

public class PolicyAuthorizationRequirement : AuthorizationHandler<PolicyAuthorizationRequirement>,
    IAuthorizationRequirement
{
    public string Name { get; }
    public PolicyAuthorizationRequirement(string name) => Name = name;

    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context,
        PolicyAuthorizationRequirement requirement)
    {
        var identity = context.User.Identities.FirstOrDefault(i =>
            i.AuthenticationType == CookieAuthenticationDefaults.AuthenticationScheme);

        if (identity == null)
        {
            context.Fail();
            return;
        }

        if (bool.Parse(identity.FindFirst(CustomClaimTypes.SuperUser)?.Value ?? "false"))
        {
            context.Succeed(this);
            return;
        }

        var policies = (identity.FindFirst(CustomClaimTypes.Policy)?.Value ?? "").Split(';');

        if (policies.Contains(Name))
            context.Succeed(this);
        else
            context.Fail();
    }
}