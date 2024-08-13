using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace IdentityService.Data.Migrations.ApplicationDbMigrations
{
    /// <inheritdoc />
    public partial class RoleDataSetAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                schema: "user_management",
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("5635bcf7-d0b1-40ad-abae-09dc5090d4c0"), null, "seller", "SELLER" },
                    { new Guid("62adc034-2709-40db-b198-170efc076cff"), null, "customer", "CUSTOMER" },
                    { new Guid("ee5b756a-719d-4251-8293-3cf25bcd31f8"), null, "admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "user_management",
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("5635bcf7-d0b1-40ad-abae-09dc5090d4c0"));

            migrationBuilder.DeleteData(
                schema: "user_management",
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("62adc034-2709-40db-b198-170efc076cff"));

            migrationBuilder.DeleteData(
                schema: "user_management",
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("ee5b756a-719d-4251-8293-3cf25bcd31f8"));
        }
    }
}
