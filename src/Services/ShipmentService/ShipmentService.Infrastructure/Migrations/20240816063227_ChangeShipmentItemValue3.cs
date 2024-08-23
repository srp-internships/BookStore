using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShipmentService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangeShipmentItemValue3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "BoohId",
                table: "ShipmentItems",
                newName: "BookId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "BookId",
                table: "ShipmentItems",
                newName: "BoohId");
        }
    }
}
