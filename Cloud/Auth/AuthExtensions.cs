    using Cloud.DAL.Database;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace Cloud.Auth;

public static class AuthExtensions
{
    public static AuthorizationPolicyBuilder RequirePolicy(this AuthorizationPolicyBuilder builder, string policy)
    {
        builder.AddRequirements(new PolicyAuthorizationRequirement(policy));
        return builder;
    }

    public static async Task UseAuthorizationAsync(this WebApplication app)
    {
        await using var scope = app.Services.CreateAsyncScope();

        var db = scope.ServiceProvider.GetRequiredService<DatabaseContext>();

        var policies = await db.Policies.ToListAsync();
        var constants = Policies.GetPolicies();
        
        var map = new Dictionary<string, bool>();

        foreach (var constant in constants)
            map.TryAdd(constant, true);

        foreach (var policy in policies)
        {
            if (!map.ContainsKey(policy.Name))
                Console.WriteLine($"УДОЛИ У ПОЛЬЗОВАТЕЛЕЙ НЕСУЩЕСТВУЮЩУЮ ПОЛИТИКУ {policy.Name}");
            else
                map.Remove(policy.Name);
        }

        foreach (var (name,flag) in map)
        {
            if (flag)
            {
                db.Policies.Add(new()
                {
                    Name = name
                });
            }
        }
        
        await db.SaveChangesAsync();
    }

    public static async Task UseDatabaseAsync(this WebApplication app, bool remove = false)
    {
        await using var scope = app.Services.CreateAsyncScope();
        
        var db = scope.ServiceProvider.GetRequiredService<DatabaseContext>();

        if (remove)
            await db.Database.EnsureDeletedAsync();
        
        await db.Database.EnsureCreatedAsync();
    }
}