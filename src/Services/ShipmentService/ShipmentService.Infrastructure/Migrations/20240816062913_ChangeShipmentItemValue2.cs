using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShipmentService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangeShipmentItemValue2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ShipmentItems",
                table: "ShipmentItems");

            migrationBuilder.RenameColumn(
                name: "ItemId",
                table: "ShipmentItems",
                newName: "BoohId");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "ShipmentItems",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_ShipmentItems",
                table: "ShipmentItems",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ShipmentItems",
                table: "ShipmentItems");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "ShipmentItems");

            migrationBuilder.RenameColumn(
                name: "BoohId",
                table: "ShipmentItems",
                newName: "ItemId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ShipmentItems",
                table: "ShipmentItems",
                column: "ItemId");
        }
    }
}
