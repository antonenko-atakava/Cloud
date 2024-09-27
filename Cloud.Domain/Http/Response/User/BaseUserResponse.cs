namespace Cloud.Domain.Http.Response.User;

public class BaseUserResponse
{
    public Guid Id { get; set; }
    public string Login { get; set; }
    public string Email { get; set; }
    public string? Phone { get; set; }
    public string? Avatar { get; set; }
}