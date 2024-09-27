using Cloud.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cloud.DAL.Configurations;

public class PolicyConfiguration : IEntityTypeConfiguration<Policy>
{
    public void Configure(EntityTypeBuilder<Policy> builder)
    {
        builder
            .ToTable("policy")
            .HasKey(x => x.Id);

        builder
            .Property(x => x.Id)
            .HasColumnName("id")
            .IsRequired();

        builder.
            Property(x => x.Name)
            .HasColumnName("name")
            .HasMaxLength(255)
            .IsRequired();
    }
}