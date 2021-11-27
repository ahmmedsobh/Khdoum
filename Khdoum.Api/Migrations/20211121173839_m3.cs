using Microsoft.EntityFrameworkCore.Migrations;

namespace Khdoum.Api.Migrations
{
    public partial class m3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_States_StateId",
                table: "Orders");

            migrationBuilder.AlterColumn<int>(
                name: "StateId",
                table: "Orders",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "DeliveryId",
                table: "Orders",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ToStateId",
                table: "Orders",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StateId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "GeneralDeliveries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FromStateId = table.Column<int>(type: "int", nullable: true),
                    ToStateId = table.Column<int>(type: "int", nullable: true),
                    DeliveryService = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GeneralDeliveries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GeneralDeliveries_States_FromStateId",
                        column: x => x.FromStateId,
                        principalTable: "States",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GeneralDeliveries_States_ToStateId",
                        column: x => x.ToStateId,
                        principalTable: "States",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_DeliveryId",
                table: "Orders",
                column: "DeliveryId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_ToStateId",
                table: "Orders",
                column: "ToStateId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_StateId",
                table: "AspNetUsers",
                column: "StateId");

            migrationBuilder.CreateIndex(
                name: "IX_GeneralDeliveries_FromStateId",
                table: "GeneralDeliveries",
                column: "FromStateId");

            migrationBuilder.CreateIndex(
                name: "IX_GeneralDeliveries_ToStateId",
                table: "GeneralDeliveries",
                column: "ToStateId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_States_StateId",
                table: "AspNetUsers",
                column: "StateId",
                principalTable: "States",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_AspNetUsers_DeliveryId",
                table: "Orders",
                column: "DeliveryId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_States_StateId",
                table: "Orders",
                column: "StateId",
                principalTable: "States",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_States_ToStateId",
                table: "Orders",
                column: "ToStateId",
                principalTable: "States",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_States_StateId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_AspNetUsers_DeliveryId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_States_StateId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_States_ToStateId",
                table: "Orders");

            migrationBuilder.DropTable(
                name: "GeneralDeliveries");

            migrationBuilder.DropIndex(
                name: "IX_Orders_DeliveryId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_ToStateId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_StateId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "DeliveryId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "ToStateId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "StateId",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<int>(
                name: "StateId",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_States_StateId",
                table: "Orders",
                column: "StateId",
                principalTable: "States",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
