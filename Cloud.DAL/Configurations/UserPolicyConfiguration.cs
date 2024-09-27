using Cloud.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cloud.DAL.Configurations;

public class UserPolicyConfiguration : IEntityTypeConfiguration<UserPolicy>
{
    public void Configure(EntityTypeBuilder<UserPolicy> builder)
    {
        builder
            .ToTable("user_policies")
            .HasKey(x => new { x.UserId, x.PolicyId });

        builder
            .Property(x => x.UserId)
            .HasColumnName("user_id")
            .IsRequired();

        builder
            .Property(x => x.PolicyId)
            .HasColumnName("policy_id")
            .IsRequired();

        builder
            .HasOne(x => x.User)
            .WithMany(x => x.UserPolicies)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasOne(x => x.Policy)
            .WithMany(x => x.UserPolicies)
            .HasForeignKey(x => x.PolicyId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}