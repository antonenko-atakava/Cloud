using System.Text.Json.Serialization;
using Cloud.Domain.Entity;

namespace Cloud.Domain.Http.Response.Directory;

public class BaseDirectoryResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Icon { get; set; }
    public string Path { get; set; }

    public Guid? ParentId { get; set; }
    public string? PathParentDirectory { get; set; }
    public virtual ICollection<ChildDirectoryResponse>? ChildrenCategories { get; set; }

    public Guid OwnerId { get; set; }
    
    [JsonIgnore]
    public virtual Entity.User Owner { get; set; }

    public DateTime AtCreate { get; set; }
    public DateTime AtUpdate { get; set; }
}