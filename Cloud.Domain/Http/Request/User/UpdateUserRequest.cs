namespace Cloud.Domain.Http.Request.User;

public class UpdateUserRequest
{
    public Guid Id {get; set;}
    public string? Login { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set; }
}