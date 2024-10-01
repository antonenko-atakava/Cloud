namespace Cloud.Domain.Entity;

public class Directory
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Path { get; set; }
    
    public Guid? ParentId { get; set; }
    public Directory Parent { get; set; }
    
    public string Icon { get; set; }
}