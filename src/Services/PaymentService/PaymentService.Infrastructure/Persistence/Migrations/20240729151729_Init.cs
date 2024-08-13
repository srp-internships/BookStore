using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PaymentService.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "payment_service");

            migrationBuilder.CreateTable(
                name: "cards",
                schema: "payment_service",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CardHolderRole = table.Column<int>(type: "int", nullable: false),
                    CardNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CardCvc = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CardHolderName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CardExpirationDate = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cards", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "payments",
                schema: "payment_service",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RequestedAtUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ProcessedAtUtc = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_payments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "transactions",
                schema: "payment_service",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    CardId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PaymentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_transactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_transactions_cards_CardId",
                        column: x => x.CardId,
                        principalSchema: "payment_service",
                        principalTable: "cards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_transactions_payments_PaymentId",
                        column: x => x.PaymentId,
                        principalSchema: "payment_service",
                        principalTable: "payments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_cards_UserId",
                schema: "payment_service",
                table: "cards",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_payments_OrderId",
                schema: "payment_service",
                table: "payments",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_transactions_CardId",
                schema: "payment_service",
                table: "transactions",
                column: "CardId");

            migrationBuilder.CreateIndex(
                name: "IX_transactions_PaymentId",
                schema: "payment_service",
                table: "transactions",
                column: "PaymentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "transactions",
                schema: "payment_service");

            migrationBuilder.DropTable(
                name: "cards",
                schema: "payment_service");

            migrationBuilder.DropTable(
                name: "payments",
                schema: "payment_service");
        }
    }
}
