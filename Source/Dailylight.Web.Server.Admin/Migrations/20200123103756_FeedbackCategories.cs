using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace Dailylight.Web.Server.Admin.Migrations
{
    public partial class FeedbackCategories : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FeedbackText",
                table: "UserFeedbacks");

            migrationBuilder.AddColumn<string>(
                name: "FeedbackCategoryId",
                table: "UserFeedbacks",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FeedbackMessage",
                table: "UserFeedbacks",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FeedbackTitle",
                table: "UserFeedbacks",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "FeedbackCategories",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    FeedbackCategory = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FeedbackCategories", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserFeedbacks_FeedbackCategoryId",
                table: "UserFeedbacks",
                column: "FeedbackCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserFeedbacks_FeedbackCategories_FeedbackCategoryId",
                table: "UserFeedbacks",
                column: "FeedbackCategoryId",
                principalTable: "FeedbackCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserFeedbacks_FeedbackCategories_FeedbackCategoryId",
                table: "UserFeedbacks");

            migrationBuilder.DropTable(
                name: "FeedbackCategories");

            migrationBuilder.DropIndex(
                name: "IX_UserFeedbacks_FeedbackCategoryId",
                table: "UserFeedbacks");

            migrationBuilder.DropColumn(
                name: "FeedbackCategoryId",
                table: "UserFeedbacks");

            migrationBuilder.DropColumn(
                name: "FeedbackMessage",
                table: "UserFeedbacks");

            migrationBuilder.DropColumn(
                name: "FeedbackTitle",
                table: "UserFeedbacks");

            migrationBuilder.AddColumn<string>(
                name: "FeedbackText",
                table: "UserFeedbacks",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
