using Microsoft.EntityFrameworkCore.Migrations;

namespace Dailylight.Web.Server.Admin.Migrations
{
    public partial class PaymentVerifiedFlag : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "PaymentVerified",
                table: "PaymentTransactions",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PaymentVerified",
                table: "PaymentTransactions");
        }
    }
}
