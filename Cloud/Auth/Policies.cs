using System.Reflection;

namespace Cloud.Auth;

public class Policies
{
    public const string USERS_CREATE = nameof(USERS_CREATE);
    public const string USERS_ADD_POLICY = nameof(USERS_ADD_POLICY);
    public const string USERS_REMOVE_POLICY = nameof(USERS_REMOVE_POLICY);
    public const string ADD_POLICY_TO_ROLE = nameof(ADD_POLICY_TO_ROLE);
    public const string REMOVE_POLICY_OF_ROLE = nameof(REMOVE_POLICY_OF_ROLE);
    public const string GET_ROLE = nameof(GET_ROLE);
    public const string CREATE_ROLE = nameof(CREATE_ROLE);
    public const string DELETE_ROLE = nameof(DELETE_ROLE);
    public const string USERS_ADD_ROLE = nameof(USERS_ADD_ROLE);
    public const string USERS_REMOVE_ROLE = nameof(USERS_REMOVE_ROLE);

    public static List<string> GetPolicies()
    {
        const BindingFlags flags = BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy;

        var constants = typeof(Policies).GetFields(flags)
            .Where(i => i is { IsLiteral: true, IsInitOnly: false })
            .ToList();

        return constants.Select(c => c.Name).ToList();
    }
}