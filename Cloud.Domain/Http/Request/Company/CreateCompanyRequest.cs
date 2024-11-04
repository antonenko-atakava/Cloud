namespace Cloud.Domain.Http.Request.Company;

public class CreateCompanyRequest
{
    public string Name { get; set; }
    public Guid OwnerId { get; set; }
}