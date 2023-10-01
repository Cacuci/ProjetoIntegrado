using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Inbound.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class _202309091957_UpdateContext : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Barcodes_PackageId",
                table: "Barcodes");

            migrationBuilder.DropColumn(
                name: "DateCreated",
                table: "Orders");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateRegister",
                table: "Products",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DateRegister",
                table: "Packages",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DateRegister",
                table: "OrderItems",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DateRegister",
                table: "OrderDocuments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "Barcodes",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateRegister",
                table: "Barcodes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_Barcodes_PackageId_Code",
                table: "Barcodes",
                columns: new[] { "PackageId", "Code" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Barcodes_PackageId_Code",
                table: "Barcodes");

            migrationBuilder.DropColumn(
                name: "DateRegister",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "DateRegister",
                table: "Packages");

            migrationBuilder.DropColumn(
                name: "DateRegister",
                table: "OrderItems");

            migrationBuilder.DropColumn(
                name: "DateRegister",
                table: "OrderDocuments");

            migrationBuilder.DropColumn(
                name: "DateRegister",
                table: "Barcodes");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateCreated",
                table: "Orders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "Barcodes",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.CreateIndex(
                name: "IX_Barcodes_PackageId",
                table: "Barcodes",
                column: "PackageId");
        }
    }
}
