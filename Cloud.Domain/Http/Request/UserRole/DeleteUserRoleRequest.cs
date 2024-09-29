namespace Cloud.Domain.Http.Request.UserRole;

public class DeleteUserRoleRequest
{
    public Guid UserId { get; set; }
    public Guid RoleId { get; set; }
}