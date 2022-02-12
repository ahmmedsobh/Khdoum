using Microsoft.EntityFrameworkCore.Migrations;

namespace Khdoum.Api.Migrations
{
    public partial class DashboardNotificationsSenderUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DashboardNotificationsSenderUser",
                table: "Notifications",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DashboardNotificationsSenderUser",
                table: "Notifications");
        }
    }
}
