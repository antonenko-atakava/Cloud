
namespace Cloud.Domain.Entity;

public class CustomDirectory
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Icon { get; set; }
    public string Path { get; set; }

    public Guid? ParentId { get; set; }
    public virtual CustomDirectory? ParentDirectory { get; set; }
    public string? PathParentDirectory { get; set; }
    public virtual ICollection<CustomDirectory>? ChildrenCategories { get; set; }

    public Guid OwnerId { get; set; }
    public virtual User Owner { get; set; }

    public DateTime AtCreate { get; set; }
    public DateTime AtUpdate { get; set; }
}