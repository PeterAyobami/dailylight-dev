using Microsoft.EntityFrameworkCore.Migrations;

namespace Dailylight.Web.Server.Admin.Migrations
{
    public partial class PaymentTransactions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PaymentTransactions",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Reference = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: true),
                    EditionId = table.Column<string>(nullable: true),
                    DevotionEditionsId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentTransactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PaymentTransactions_DevotionEditions_DevotionEditionsId",
                        column: x => x.DevotionEditionsId,
                        principalTable: "DevotionEditions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PaymentTransactions_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PaymentTransactions_DevotionEditionsId",
                table: "PaymentTransactions",
                column: "DevotionEditionsId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentTransactions_UserId",
                table: "PaymentTransactions",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PaymentTransactions");
        }
    }
}
