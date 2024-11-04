namespace Cloud.Domain.Http.Request.Directory;

public class GetByUserDirectoryRequest
{
    public string Path { get; set; }
    public string DirectoryName { get; set; }
    public Guid UserId { get; set; }
}