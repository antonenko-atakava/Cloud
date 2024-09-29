namespace Cloud.Domain.Entity;

public class Role
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public virtual ICollection<RolePolicy>? RolePolicies { get; set; }
    public virtual ICollection<UserRole>? UserRoles { get; set; }
}