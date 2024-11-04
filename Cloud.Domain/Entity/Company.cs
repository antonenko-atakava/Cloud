namespace Cloud.Domain.Entity;

public class Company
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Guid OwnerId { get; set; }
    public virtual User Owner { get; set; }
    public virtual ICollection<UserCompany>? UserCompany { get; set; }
    public virtual ICollection<CompanyRole>? Roles { get; set; }
    public virtual ICollection<CustomDirectory>? Directories { get; set; }
}