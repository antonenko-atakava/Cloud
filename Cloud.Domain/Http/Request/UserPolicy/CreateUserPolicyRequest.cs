namespace Cloud.Domain.Http.Request.UserPolicy;

public class CreateUserPolicyRequest
{
    public Guid UserId { get; set; }
    public Guid PolicyId { get; set; }
}