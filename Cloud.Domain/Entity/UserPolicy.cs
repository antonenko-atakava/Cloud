namespace Cloud.Domain.Entity;

public class UserPolicy
{
    public Guid UserId { get; set; }
    public Guid PolicyId { get; set; }

    public virtual User User { get; set; }

    public virtual Policy Policy { get; set; }
}