namespace Cloud.Domain.Entity;

public class User
{
    public Guid Id { get; set; }
    public string Login { get; set; }
    public string Password { get; set; }
    public string Salt { get; set; }
    public string Email { get; set; }
    public string? Phone { get; set; }
    public string Avatar { get; set; }
    public DateTime Created { get; set; }
    public DateTime Modified { get; set; }
    public bool IsSuperUser { get; set; }
    public virtual ICollection<UserRole>? UserRoles { get; set; }
}