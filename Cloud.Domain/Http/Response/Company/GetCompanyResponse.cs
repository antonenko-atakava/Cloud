using Cloud.Domain.Http.Response.User;

namespace Cloud.Domain.Http.Response.Company;

public class GetCompanyResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public virtual BaseUserResponse Owner { get; set; }
    public virtual ICollection<BaseUserResponse>? Users { get; set; }
    public virtual ICollection<Entity.Role>? Role { get; set; }
}