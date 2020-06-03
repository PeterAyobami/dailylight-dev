using Microsoft.EntityFrameworkCore.Migrations;

namespace Dailylight.Web.Server.Admin.Migrations
{
    public partial class EditionYearAndImageUrlColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EditionYear",
                table: "DevotionEditions",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "DevotionEditions",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EditionYear",
                table: "DevotionEditions");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "DevotionEditions");
        }
    }
}
