using Microsoft.EntityFrameworkCore.Migrations;

namespace Dailylight.Web.Server.Admin.Migrations
{
    public partial class DevotionEditionRelationals : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DevotionEditionId",
                table: "Devotions",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "DevotionEditions",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Devotions_DevotionEditionId",
                table: "Devotions",
                column: "DevotionEditionId");

            migrationBuilder.CreateIndex(
                name: "IX_DevotionEditions_ApplicationUserId",
                table: "DevotionEditions",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_DevotionEditions_AspNetUsers_ApplicationUserId",
                table: "DevotionEditions",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Devotions_DevotionEditions_DevotionEditionId",
                table: "Devotions",
                column: "DevotionEditionId",
                principalTable: "DevotionEditions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DevotionEditions_AspNetUsers_ApplicationUserId",
                table: "DevotionEditions");

            migrationBuilder.DropForeignKey(
                name: "FK_Devotions_DevotionEditions_DevotionEditionId",
                table: "Devotions");

            migrationBuilder.DropIndex(
                name: "IX_Devotions_DevotionEditionId",
                table: "Devotions");

            migrationBuilder.DropIndex(
                name: "IX_DevotionEditions_ApplicationUserId",
                table: "DevotionEditions");

            migrationBuilder.DropColumn(
                name: "DevotionEditionId",
                table: "Devotions");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "DevotionEditions");
        }
    }
}
