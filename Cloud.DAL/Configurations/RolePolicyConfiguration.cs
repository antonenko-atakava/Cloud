using Cloud.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cloud.DAL.Configurations;

public class RolePolicyConfiguration : IEntityTypeConfiguration<RolePolicy>
{
    public void Configure(EntityTypeBuilder<RolePolicy> builder)
    {
        builder
            .ToTable("role_policies")
            .HasKey(x => new { x.RoleId, x.PolicyId });

        builder
            .Property(x => x.RoleId)
            .HasColumnName("role_id")
            .IsRequired();

        builder
            .Property(x => x.PolicyId)
            .HasColumnName("policy_id")
            .IsRequired();

        builder
            .HasOne(x => x.Role)
            .WithMany(x => x.RolePolicies)
            .HasForeignKey(x => x.RoleId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasOne(x => x.Policy)
            .WithMany(x => x.RolePolicies)
            .HasForeignKey(x => x.PolicyId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}