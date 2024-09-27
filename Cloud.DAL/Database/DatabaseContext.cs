using Microsoft.Extensions.Configuration;
using Cloud.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace Cloud.DAL.Database;

public class DatabaseContext : DbContext
{
    private readonly IConfiguration _configuration;

    public DatabaseContext(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Policy> Policies { get; set; }
    public DbSet<UserPolicy> UserPolicies { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
            .UseNpgsql(_configuration.GetConnectionString("DefaultConnection"))
            .UseLazyLoadingProxies();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(DatabaseContext).Assembly);
    }
}