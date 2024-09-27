using Cloud.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cloud.DAL.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder
            .ToTable("Users")
            .HasKey(x => x.Id);

        builder
            .Property(x => x.Id)
            .HasColumnName("id")
            .ValueGeneratedOnAdd()
            .IsRequired();

        builder
            .Property(x => x.Login)
            .HasColumnName("login")
            .HasMaxLength(255)
            .IsRequired();

        builder
            .Property(x => x.Password)
            .HasColumnName("password")
            .IsRequired();

        builder
            .Property(x => x.Salt)
            .HasColumnName("salt")
            .IsRequired();

        builder
            .Property(x => x.Email)
            .HasColumnName("email")
            .HasMaxLength(255)
            .IsRequired();

        builder
            .Property(x => x.Phone)
            .HasColumnName("phone")
            .HasMaxLength(15);

        builder
            .Property(x => x.Avatar)
            .HasColumnName("avatar")
            .IsRequired();

        builder
            .Property(x => x.Created)
            .HasColumnName("created")
            .IsRequired();

        builder
            .Property(x => x.Modified)
            .HasColumnName("modified")
            .IsRequired();

        builder
            .Property(x => x.IsSuperUser)
            .HasColumnName("isSuperUser")
            .IsRequired();
    }
}