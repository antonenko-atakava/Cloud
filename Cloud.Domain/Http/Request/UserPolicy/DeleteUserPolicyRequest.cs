namespace Cloud.Domain.Http.Request.UserPolicy;

public class DeleteUserPolicyRequest
{
    public Guid UserId { get; set; }
    public Guid PolicyId { get; set; }
}