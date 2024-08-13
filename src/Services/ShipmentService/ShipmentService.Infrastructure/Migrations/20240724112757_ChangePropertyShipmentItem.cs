using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShipmentService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangePropertyShipmentItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BookName",
                table: "ShipmentItems",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BookName",
                table: "ShipmentItems");
        }
    }
}
