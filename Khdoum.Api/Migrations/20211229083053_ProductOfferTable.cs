using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Khdoum.Api.Migrations
{
    public partial class ProductOfferTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProductOffer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaximumUseCount = table.Column<int>(type: "int", nullable: false),
                    UsedCount = table.Column<int>(type: "int", nullable: false),
                    Discount = table.Column<int>(type: "int", nullable: false),
                    DiscountType = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    ExpiryDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Img = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MarketProductId = table.Column<long>(type: "bigint", nullable: false),
                    MarketProductUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MarketProductProductId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductOffer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductOffer_MarketProducts_MarketProductUserId_MarketProductProductId",
                        columns: x => new { x.MarketProductUserId, x.MarketProductProductId },
                        principalTable: "MarketProducts",
                        principalColumns: new[] { "UserId", "ProductId" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductOffer_MarketProductUserId_MarketProductProductId",
                table: "ProductOffer",
                columns: new[] { "MarketProductUserId", "MarketProductProductId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductOffer");
        }
    }
}
