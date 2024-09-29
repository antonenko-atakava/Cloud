namespace Cloud.Domain.Http.Request.RolePolicy;

public class CreateRolePolicyRequest
{
    public Guid RoleId { get; set; }
    public Guid PolicyId { get; set; }
}