using Microsoft.AspNetCore.Http;

namespace Cloud.Domain.Http.Request.User;

public class UpdateAvatarUserRequest
{
    public Guid Id { get; set; }
    public FormFile File { get; set; }
}