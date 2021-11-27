﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace Khdoum.Api.Migrations
{
    public partial class addPageLink : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PageLink",
                table: "Categories",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PageLink",
                table: "Categories");
        }
    }
}
