namespace Cloud.Domain.Entity;

public class CompanyRole
{
    public Guid Id { get; set; }
    public string Name { get; set; }

    public Guid CompanyId { get; set; }
    public virtual Company Company { get; set; }
}