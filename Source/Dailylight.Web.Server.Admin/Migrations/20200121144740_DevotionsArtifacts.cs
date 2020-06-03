using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Dailylight.Web.Server.Admin.Migrations
{
    public partial class DevotionsArtifacts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EditionYear",
                table: "DevotionEditions");

            migrationBuilder.AlterColumn<int>(
                name: "Edition",
                table: "DevotionEditions",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<float>(
                name: "EditionPrice",
                table: "DevotionEditions",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.CreateTable(
                name: "DevotionReviews",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserId = table.Column<string>(nullable: true),
                    ReviewText = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DevotionReviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DevotionReviews_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PurchasedEditions",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserId = table.Column<string>(nullable: true),
                    EditionId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchasedEditions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PurchasedEditions_DevotionEditions_EditionId",
                        column: x => x.EditionId,
                        principalTable: "DevotionEditions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PurchasedEditions_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserFeedbacks",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserId = table.Column<string>(nullable: true),
                    FeedbackText = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserFeedbacks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserFeedbacks_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DevotionReviews_UserId",
                table: "DevotionReviews",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchasedEditions_EditionId",
                table: "PurchasedEditions",
                column: "EditionId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchasedEditions_UserId",
                table: "PurchasedEditions",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserFeedbacks_UserId",
                table: "UserFeedbacks",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DevotionReviews");

            migrationBuilder.DropTable(
                name: "PurchasedEditions");

            migrationBuilder.DropTable(
                name: "UserFeedbacks");

            migrationBuilder.DropColumn(
                name: "EditionPrice",
                table: "DevotionEditions");

            migrationBuilder.AlterColumn<string>(
                name: "Edition",
                table: "DevotionEditions",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<DateTime>(
                name: "EditionYear",
                table: "DevotionEditions",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
