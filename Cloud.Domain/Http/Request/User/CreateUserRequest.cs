namespace Cloud.Domain.Http.Request.User;

public class CreateUserRequest
{
    public string Login { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
}