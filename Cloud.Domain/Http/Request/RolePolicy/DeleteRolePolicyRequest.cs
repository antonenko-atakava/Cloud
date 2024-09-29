namespace Cloud.Domain.Http.Request.RolePolicy;

public class DeleteRolePolicyRequest
{
    public Guid RoleId { get; set; }
    public Guid PolicyId { get; set; }
}