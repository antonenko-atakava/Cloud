namespace Cloud.Domain.Http.Request.Directory;

public class UpdatePathDirectoryRequest
{
    public Guid Id { get; set; }
    public string NewPath { get; set; }
}