namespace Cloud.Domain.Entity;

public class UserCompany
{
    public Guid UserId { get; set; }
    public virtual User User { get; set; }

    public Guid CompanyId { get; set; }
    public virtual Company Company { get; set; }
}