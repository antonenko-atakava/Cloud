using System.Security.Claims;
using Cloud.DAL;
using Cloud.DAL.Database;
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

            var claim = identity.FindFirst(CustomClaimTypes.Identifier)?.Type;

            var userId = Guid.Parse(claim ?? throw new InvalidOperationException());

            var user = await db.Users.FirstOrDefaultAsync(i => i.Id == userId);

            if (user == null)
                throw new NullReferenceException(nameof(user));

            var policies = await db.UserPolicies
                .Where(i => i.UserId == userId)
                .Include(userPolicy => userPolicy.Policy)
                .ToListAsync();

            identity = new ClaimsIdentity(identity, new[]
            {
                new Claim(CustomClaimTypes.Policy, string.Join(";", policies.Select(p => p.Policy.Name))),
                new Claim(CustomClaimTypes.SuperUser, user.IsSuperUser.ToString())
            });
            
            context.ReplacePrincipal(new ClaimsPrincipal(identity));
        }
        catch
        {
            context.RejectPrincipal();
        }
    }
}