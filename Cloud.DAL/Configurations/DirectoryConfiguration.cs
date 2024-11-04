using Cloud.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cloud.DAL.Configurations;

public class DirectoryConfiguration : IEntityTypeConfiguration<CustomDirectory>
{
    public void Configure(EntityTypeBuilder<CustomDirectory> builder)
    {
        builder
            .ToTable("directory")
            .HasKey(x => x.Id);

        builder
            .Property(x => x.Id)
            .HasColumnName("id")
            .ValueGeneratedOnAdd()
            .IsRequired();

        builder
            .Property(x => x.Name)
            .HasColumnName("name")
            .HasMaxLength(255)
            .IsRequired();

        builder
            .Property(x => x.Path)
            .HasColumnName("path")
            .IsRequired();

        builder
            .Property(x => x.Icon)
            .HasColumnName("icon")
            .IsRequired();

        builder
            .Property(x => x.ParentId)
            .HasColumnName("parent_id");

        builder
            .Property(x => x.PathParentDirectory)
            .HasColumnName("path_parent_directory");

        builder.HasOne(e => e.ParentDirectory)
            .WithMany(x => x.ChildrenCategories)
            .HasForeignKey(e => e.ParentId)
            .OnDelete(DeleteBehavior.Cascade); 

        builder
            .Property(x => x.OwnerId)
            .HasColumnName("owner_id")
            .IsRequired();

        builder
            .HasOne(e => e.Owner)
            .WithMany(e => e.Directories)
            .HasForeignKey(e => e.OwnerId);

        builder
            .Property(x => x.AtCreate)
            .HasColumnName("at_create")
            .IsRequired();

        builder
            .Property(x => x.AtUpdate)
            .HasColumnName("at_update")
            .IsRequired();
    }
}