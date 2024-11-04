namespace Cloud.Domain.Http.Request.Directory;

public class GetByNameDirectoryRequest
{
    public string NameDirectory { get; set; }
    public Guid UserId { get; set; }
}