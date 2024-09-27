using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cloud.Domain.Entity;

public class Policy
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public virtual ICollection<UserPolicy>? UserPolicies { get; set; }   
}