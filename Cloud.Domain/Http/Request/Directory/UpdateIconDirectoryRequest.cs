using Microsoft.AspNetCore.Http;

namespace Cloud.Domain.Http.Request.Directory;

public class UpdateIconDirectoryRequest
{
    public Guid Id { get; set; }
    public IFormFile Icon { get; set; }
}