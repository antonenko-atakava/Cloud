using System.Security.Claims;
using Cloud.DAL.Database;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

namespace Cloud.Auth;

public class CustomCookieAuthenticationEvents : CookieAuthenticationEvents
{
    public override async Task ValidatePrincipal(CookieValidatePrincipalContext context)
    {
        try
        {
            var db = context.HttpContext.RequestServices.GetRequiredService<DatabaseContext>();

            var identity = context.Principal?.Identities.FirstOrDefault(i =>
                i.AuthenticationType == CookieAuthenticationDefaults.AuthenticationScheme);

            if (identity == null)
            {
                return;
            }

            var claim = identity.FindFirst(CustomClaimTypes.Identifier)?.Value;

            var userId = Guid.Parse(claim ?? throw new InvalidOperationException());

            var user = await db.Users
                .Include(u => u.UserRoles)!
                .ThenInclude(ur => ur.Role)
                .FirstOrDefaultAsync(i => i.Id == userId);

            if (user == null)
                throw new NullReferenceException(nameof(user));

            var policies = new List<string>();

            if (user.UserRoles == null)
                throw new NullReferenceException(nameof(user));
            
            foreach (var role in user.UserRoles)
            {
                var rolePolicies = await db.RolePolicies
                    .Where(i => i.RoleId == role.RoleId)
                    .Include(up => up.Policy)
                    .ToListAsync();

                foreach (var rolePolicy in rolePolicies)
                {
                    policies.Add(rolePolicy.Policy.Name);
                }
            }

            // var policies = await db.UserPolicies
            //     .Where(i => i.UserId == userId)
            //     .Include(userPolicy => userPolicy.Policy)
            //     .ToListAsync();

            identity = new ClaimsIdentity(identity, new[]
            {
                new Claim(CustomClaimTypes.Policy, string.Join(";", policies)),
                new Claim(CustomClaimTypes.SuperUser, user.IsSuperUser.ToString())
            });

            context.ReplacePrincipal(new ClaimsPrincipal(identity));
        }
        catch
        {
            context.RejectPrincipal();
        }
    }

    public override Task RedirectToLogin(RedirectContext<CookieAuthenticationOptions> context)
    {
        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
        return Task.CompletedTask;
    }

    public override Task RedirectToAccessDenied(RedirectContext<CookieAuthenticationOptions> context)
    {
        context.Response.StatusCode = StatusCodes.Status403Forbidden;
        return Task.CompletedTask;
    }
}