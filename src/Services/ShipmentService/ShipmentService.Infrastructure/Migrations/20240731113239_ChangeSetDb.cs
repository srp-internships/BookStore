using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShipmentService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangeSetDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShipmentItems_Shipments_ShipmentId",
                table: "ShipmentItems");

            migrationBuilder.DropForeignKey(
                name: "FK_Shipments_ShippingAddresses_ShippingAddressId",
                table: "Shipments");

            migrationBuilder.AlterColumn<string>(
                name: "Street",
                table: "ShippingAddresses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Country",
                table: "ShippingAddresses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "City",
                table: "ShippingAddresses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "OrderStatus",
                table: "Shipments",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "Quantity",
                table: "ShipmentItems",
                type: "int",
                nullable: false,
                defaultValue: 1,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_ShipmentItems_Shipments_ShipmentId",
                table: "ShipmentItems",
                column: "ShipmentId",
                principalTable: "Shipments",
                principalColumn: "ShipmentId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Shipments_ShippingAddresses_ShippingAddressId",
                table: "Shipments",
                column: "ShippingAddressId",
                principalTable: "ShippingAddresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShipmentItems_Shipments_ShipmentId",
                table: "ShipmentItems");

            migrationBuilder.DropForeignKey(
                name: "FK_Shipments_ShippingAddresses_ShippingAddressId",
                table: "Shipments");

            migrationBuilder.AlterColumn<string>(
                name: "Street",
                table: "ShippingAddresses",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Country",
                table: "ShippingAddresses",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "City",
                table: "ShippingAddresses",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "OrderStatus",
                table: "Shipments",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "Quantity",
                table: "ShipmentItems",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: 1);

            migrationBuilder.AddForeignKey(
                name: "FK_ShipmentItems_Shipments_ShipmentId",
                table: "ShipmentItems",
                column: "ShipmentId",
                principalTable: "Shipments",
                principalColumn: "ShipmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Shipments_ShippingAddresses_ShippingAddressId",
                table: "Shipments",
                column: "ShippingAddressId",
                principalTable: "ShippingAddresses",
                principalColumn: "Id");
        }
    }
}
