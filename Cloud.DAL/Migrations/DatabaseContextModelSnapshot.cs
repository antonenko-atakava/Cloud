﻿// <auto-generated />
using System;
using Cloud.DAL.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Cloud.DAL.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    partial class DatabaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Proxies:ChangeTracking", false)
                .HasAnnotation("Proxies:CheckEquality", false)
                .HasAnnotation("Proxies:LazyLoading", true)
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Cloud.Domain.Entity.Company", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(155)
                        .HasColumnType("character varying(155)")
                        .HasColumnName("name");

                    b.Property<Guid>("OwnerId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("OwnerId");

                    b.ToTable("company", (string)null);
                });

            modelBuilder.Entity("Cloud.Domain.Entity.CompanyRole", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<Guid>("CompanyId")
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("character varying(150)")
                        .HasColumnName("name");

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.ToTable("company_roles", (string)null);
                });

            modelBuilder.Entity("Cloud.Domain.Entity.CustomDirectory", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<DateTime>("AtCreate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("at_create");

                    b.Property<DateTime>("AtUpdate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("at_update");

                    b.Property<Guid?>("CompanyId")
                        .HasColumnType("uuid");

                    b.Property<string>("Icon")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("icon");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("name");

                    b.Property<Guid>("OwnerId")
                        .HasColumnType("uuid")
                        .HasColumnName("owner_id");

                    b.Property<Guid?>("ParentId")
                        .HasColumnType("uuid")
                        .HasColumnName("parent_id");

                    b.Property<string>("Path")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("path");

                    b.Property<string>("PathParentDirectory")
                        .HasColumnType("text")
                        .HasColumnName("path_parent_directory");

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.HasIndex("OwnerId");

                    b.HasIndex("ParentId");

                    b.ToTable("directory", (string)null);
                });

            modelBuilder.Entity("Cloud.Domain.Entity.Policy", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("name");

                    b.HasKey("Id");

                    b.ToTable("policy", (string)null);
                });

            modelBuilder.Entity("Cloud.Domain.Entity.Role", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("name");

                    b.HasKey("Id");

                    b.ToTable("roles", (string)null);
                });

            modelBuilder.Entity("Cloud.Domain.Entity.RolePolicy", b =>
                {
                    b.Property<Guid>("RoleId")
                        .HasColumnType("uuid")
                        .HasColumnName("role_id");

                    b.Property<Guid>("PolicyId")
                        .HasColumnType("uuid")
                        .HasColumnName("policy_id");

                    b.HasKey("RoleId", "PolicyId");

                    b.HasIndex("PolicyId");

                    b.ToTable("role_policies", (string)null);
                });

            modelBuilder.Entity("Cloud.Domain.Entity.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Avatar")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("avatar");

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("email");

                    b.Property<bool>("IsSuperUser")
                        .HasColumnType("boolean")
                        .HasColumnName("isSuperUser");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("login");

                    b.Property<DateTime>("Modified")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("modified");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("password");

                    b.Property<string>("Phone")
                        .HasMaxLength(15)
                        .HasColumnType("character varying(15)")
                        .HasColumnName("phone");

                    b.Property<string>("Salt")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("salt");

                    b.HasKey("Id");

                    b.ToTable("users", (string)null);
                });

            modelBuilder.Entity("Cloud.Domain.Entity.UserCompany", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid")
                        .HasColumnName("user_id");

                    b.Property<Guid>("CompanyId")
                        .HasColumnType("uuid")
                        .HasColumnName("company_id");

                    b.HasKey("UserId", "CompanyId");

                    b.HasIndex("CompanyId");

                    b.ToTable("user_company", (string)null);
                });

            modelBuilder.Entity("Cloud.Domain.Entity.UserRole", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid")
                        .HasColumnName("user_id");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uuid")
                        .HasColumnName("role_id");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("user_role", (string)null);
                });

            modelBuilder.Entity("Cloud.Domain.Entity.Company", b =>
                {
                    b.HasOne("Cloud.Domain.Entity.User", "Owner")
                        .WithMany()
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("Cloud.Domain.Entity.CompanyRole", b =>
                {
                    b.HasOne("Cloud.Domain.Entity.Company", "Company")
                        .WithMany("Roles")
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Company");
                });

            modelBuilder.Entity("Cloud.Domain.Entity.CustomDirectory", b =>
                {
                    b.HasOne("Cloud.Domain.Entity.Company", null)
                        .WithMany("Directories")
                        .HasForeignKey("CompanyId");

                    b.HasOne("Cloud.Domain.Entity.User", "Owner")
                        .WithMany("Directories")
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Cloud.Domain.Entity.CustomDirectory", "ParentDirectory")
                        .WithMany("ChildrenCategories")
                        .HasForeignKey("ParentId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("Owner");

                    b.Navigation("ParentDirectory");
                });

            modelBuilder.Entity("Cloud.Domain.Entity.RolePolicy", b =>
                {
                    b.HasOne("Cloud.Domain.Entity.Policy", "Policy")
                        .WithMany("RolePolicies")
                        .HasForeignKey("PolicyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Cloud.Domain.Entity.Role", "Role")
                        .WithMany("RolePolicies")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Policy");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("Cloud.Domain.Entity.UserCompany", b =>
                {
                    b.HasOne("Cloud.Domain.Entity.Company", "Company")
                        .WithMany("UserCompany")
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Cloud.Domain.Entity.User", "User")
                        .WithMany("UserCompany")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Company");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Cloud.Domain.Entity.UserRole", b =>
                {
                    b.HasOne("Cloud.Domain.Entity.Role", "Role")
                        .WithMany("UserRoles")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Cloud.Domain.Entity.User", "User")
                        .WithMany("UserRoles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Cloud.Domain.Entity.Company", b =>
                {
                    b.Navigation("Directories");

                    b.Navigation("Roles");

                    b.Navigation("UserCompany");
                });

            modelBuilder.Entity("Cloud.Domain.Entity.CustomDirectory", b =>
                {
                    b.Navigation("ChildrenCategories");
                });

            modelBuilder.Entity("Cloud.Domain.Entity.Policy", b =>
                {
                    b.Navigation("RolePolicies");
                });

            modelBuilder.Entity("Cloud.Domain.Entity.Role", b =>
                {
                    b.Navigation("RolePolicies");

                    b.Navigation("UserRoles");
                });

            modelBuilder.Entity("Cloud.Domain.Entity.User", b =>
                {
                    b.Navigation("Directories");

                    b.Navigation("UserCompany");

                    b.Navigation("UserRoles");
                });
#pragma warning restore 612, 618
        }
    }
}
