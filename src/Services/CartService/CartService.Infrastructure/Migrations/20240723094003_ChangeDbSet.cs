using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CartService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangeDbSet : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CartId1",
                table: "Items",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CartId2",
                table: "Items",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Items_CartId1",
                table: "Items",
                column: "CartId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Carts_CartId1",
                table: "Items",
                column: "CartId1",
                principalTable: "Carts",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_Carts_CartId1",
                table: "Items");

            migrationBuilder.DropIndex(
                name: "IX_Items_CartId1",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "CartId1",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "CartId2",
                table: "Items");
        }
    }
}
