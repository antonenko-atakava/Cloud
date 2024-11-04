    using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cloud.DAL.Migrations
{
    /// <inheritdoc />
    public partial class Add_Company_Entities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "company",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(155)", maxLength: 155, nullable: false),
                    OwnerId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_company", x => x.id);
                    table.ForeignKey(
                        name: "FK_company_users_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "company_roles",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    CompanyId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_company_roles", x => x.id);
                    table.ForeignKey(
                        name: "FK_company_roles_company_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "company",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "directory",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    icon = table.Column<string>(type: "text", nullable: false),
                    path = table.Column<string>(type: "text", nullable: false),
                    parent_id = table.Column<Guid>(type: "uuid", nullable: true),
                    path_parent_directory = table.Column<string>(type: "text", nullable: true),
                    owner_id = table.Column<Guid>(type: "uuid", nullable: false),
                    at_create = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    at_update = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_directory", x => x.id);
                    table.ForeignKey(
                        name: "FK_directory_company_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "company",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_directory_directory_parent_id",
                        column: x => x.parent_id,
                        principalTable: "directory",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_directory_users_owner_id",
                        column: x => x.owner_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user_company",
                columns: table => new
                {
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    company_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_company", x => new { x.user_id, x.company_id });
                    table.ForeignKey(
                        name: "FK_user_company_company_company_id",
                        column: x => x.company_id,
                        principalTable: "company",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_user_company_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_company_OwnerId",
                table: "company",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_company_roles_CompanyId",
                table: "company_roles",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_directory_CompanyId",
                table: "directory",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_directory_owner_id",
                table: "directory",
                column: "owner_id");

            migrationBuilder.CreateIndex(
                name: "IX_directory_parent_id",
                table: "directory",
                column: "parent_id");

            migrationBuilder.CreateIndex(
                name: "IX_user_company_company_id",
                table: "user_company",
                column: "company_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "company_roles");

            migrationBuilder.DropTable(
                name: "directory");

            migrationBuilder.DropTable(
                name: "user_company");

            migrationBuilder.DropTable(
                name: "company");
        }
    }
}
