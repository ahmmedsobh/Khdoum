using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Khdoum.Api.Migrations
{
    public partial class dproductoffer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductOffers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProductOffers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Discount = table.Column<int>(type: "int", nullable: false),
                    DiscountType = table.Column<int>(type: "int", nullable: false),
                    ExpiryDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Img = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    MarketProductId = table.Column<long>(type: "bigint", nullable: false),
                    MarketProductProductId = table.Column<long>(type: "bigint", nullable: false),
                    MarketProductUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MaximumUseCount = table.Column<int>(type: "int", nullable: false),
                    UsedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductOffers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductOffers_MarketProducts_MarketProductUserId_MarketProductProductId",
                        columns: x => new { x.MarketProductUserId, x.MarketProductProductId },
                        principalTable: "MarketProducts",
                        principalColumns: new[] { "UserId", "ProductId" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductOffers_MarketProductUserId_MarketProductProductId",
                table: "ProductOffers",
                columns: new[] { "MarketProductUserId", "MarketProductProductId" });
        }
    }
}
