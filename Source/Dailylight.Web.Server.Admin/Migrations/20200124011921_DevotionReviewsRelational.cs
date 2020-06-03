using Microsoft.EntityFrameworkCore.Migrations;

namespace Dailylight.Web.Server.Admin.Migrations
{
    public partial class DevotionReviewsRelational : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DevotionId",
                table: "DevotionReviews",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_DevotionReviews_DevotionId",
                table: "DevotionReviews",
                column: "DevotionId");

            migrationBuilder.AddForeignKey(
                name: "FK_DevotionReviews_Devotions_DevotionId",
                table: "DevotionReviews",
                column: "DevotionId",
                principalTable: "Devotions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DevotionReviews_Devotions_DevotionId",
                table: "DevotionReviews");

            migrationBuilder.DropIndex(
                name: "IX_DevotionReviews_DevotionId",
                table: "DevotionReviews");

            migrationBuilder.DropColumn(
                name: "DevotionId",
                table: "DevotionReviews");
        }
    }
}
