namespace Cloud.Domain.Http.Request.Directory;

public class CreateDirectoryRequest
{
    public Guid UserId { get; set; }
    public Guid ParentId { get; set; }
    public string Name { get; set; }
    public string Path { get; set; }
}