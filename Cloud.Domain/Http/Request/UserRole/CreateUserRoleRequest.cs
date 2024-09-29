namespace Cloud.Domain.Http.Request.UserRole;

public class CreateUserRoleRequest
{
    public Guid UserId { get; set; }
    public Guid RoleId { get; set; }
}