using System.Reflection;

namespace Cloud.Auth;

public class Policies
{
    public const string USERS_CREATE = nameof(USERS_CREATE);
    public const string USERS_ADD_POLICY = nameof(USERS_ADD_POLICY);
    public const string USERS_REMOVE_POLICY = nameof(USERS_REMOVE_POLICY);

    public static List<string> GetPolicies()
    {
        const BindingFlags flags = BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy;

        var constants = typeof(Policies).GetFields(flags)
            .Where(i => i is { IsLiteral: true, IsInitOnly: false })
            .ToList();

        return constants.Select(c => c.Name).ToList();
    }
}