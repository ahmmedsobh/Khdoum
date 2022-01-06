using Microsoft.EntityFrameworkCore.Migrations;

namespace Khdoum.Api.Migrations
{
    public partial class addProductOfferToApplicationContext : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductOffer_MarketProducts_MarketProductUserId_MarketProductProductId",
                table: "ProductOffer");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductOffer",
                table: "ProductOffer");

            migrationBuilder.RenameTable(
                name: "ProductOffer",
                newName: "ProductOffers");

            migrationBuilder.RenameIndex(
                name: "IX_ProductOffer_MarketProductUserId_MarketProductProductId",
                table: "ProductOffers",
                newName: "IX_ProductOffers_MarketProductUserId_MarketProductProductId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductOffers",
                table: "ProductOffers",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductOffers_MarketProducts_MarketProductUserId_MarketProductProductId",
                table: "ProductOffers",
                columns: new[] { "MarketProductUserId", "MarketProductProductId" },
                principalTable: "MarketProducts",
                principalColumns: new[] { "UserId", "ProductId" },
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductOffers_MarketProducts_MarketProductUserId_MarketProductProductId",
                table: "ProductOffers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductOffers",
                table: "ProductOffers");

            migrationBuilder.RenameTable(
                name: "ProductOffers",
                newName: "ProductOffer");

            migrationBuilder.RenameIndex(
                name: "IX_ProductOffers_MarketProductUserId_MarketProductProductId",
                table: "ProductOffer",
                newName: "IX_ProductOffer_MarketProductUserId_MarketProductProductId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductOffer",
                table: "ProductOffer",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductOffer_MarketProducts_MarketProductUserId_MarketProductProductId",
                table: "ProductOffer",
                columns: new[] { "MarketProductUserId", "MarketProductProductId" },
                principalTable: "MarketProducts",
                principalColumns: new[] { "UserId", "ProductId" },
                onDelete: ReferentialAction.Cascade);
        }
    }
}
