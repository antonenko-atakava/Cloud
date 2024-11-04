using Cloud.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cloud.DAL.Configurations;

public class CompanyConfiguration : IEntityTypeConfiguration<Company>
{
    public void Configure(EntityTypeBuilder<Company> builder)
    {
        builder
            .ToTable("company")
            .HasKey(company => company.Id);

        builder.Property(company => company.Id)
            .HasColumnName("id")
            .ValueGeneratedOnAdd();

        builder.Property(company => company.Name)
            .HasColumnName("name")
            .HasMaxLength(155)
            .IsRequired();
        
        builder.HasOne(c => c.Owner)
            .WithMany()
            .HasForeignKey(c => c.OwnerId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasMany(c => c.Roles)
            .WithOne(c => c.Company)
            .HasForeignKey(c => c.CompanyId)
            .OnDelete(DeleteBehavior.Cascade);
        
    }
}