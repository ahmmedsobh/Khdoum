using Microsoft.EntityFrameworkCore.Migrations;

namespace Khdoum.Api.Migrations
{
    public partial class senderUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DashboardNotificationsSenderUser",
                table: "Notifications",
                newName: "SenderUser");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SenderUser",
                table: "Notifications",
                newName: "DashboardNotificationsSenderUser");
        }
    }
}
