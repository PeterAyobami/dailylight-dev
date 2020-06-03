using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Dailylight.Web.Server.Admin.Migrations
{
    public partial class DevotionEditions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DevotionEditions",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Edition = table.Column<string>(nullable: true),
                    EditionYear = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DevotionEditions", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "DevotionEditions");

            migrationBuilder.DropTable(
                name: "Devotions");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
