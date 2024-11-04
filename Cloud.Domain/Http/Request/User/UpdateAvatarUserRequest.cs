using Microsoft.AspNetCore.Http;

namespace Cloud.Domain.Http.Request.User;

public class UpdateAvatarUserRequest
{
    public Guid Id { get; set; }
    public IFormFile File { get; set; }
}