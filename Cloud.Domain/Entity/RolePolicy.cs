namespace Cloud.Domain.Entity;

public class RolePolicy
{
    public Guid RoleId { get; set; }
    public virtual Role Role { get; set; }

    public Guid PolicyId { get; set; }
    public virtual Policy Policy { get; set; }
}