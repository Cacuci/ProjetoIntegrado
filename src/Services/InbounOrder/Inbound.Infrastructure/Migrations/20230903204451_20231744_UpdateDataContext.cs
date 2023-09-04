using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Inbound.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class _20231744_UpdateDataContext : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Barcodes_Packages_PackageId",
                table: "Barcodes");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderDocuments_Orders_OrderId",
                table: "OrderDocuments");

            migrationBuilder.DropForeignKey(
                name: "FK_Packages_Products_ProductId",
                table: "Packages");

            migrationBuilder.AddForeignKey(
                name: "FK_Barcodes_Packages_PackageId",
                table: "Barcodes",
                column: "PackageId",
                principalTable: "Packages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDocuments_Orders_OrderId",
                table: "OrderDocuments",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Packages_Products_ProductId",
                table: "Packages",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Barcodes_Packages_PackageId",
                table: "Barcodes");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderDocuments_Orders_OrderId",
                table: "OrderDocuments");

            migrationBuilder.DropForeignKey(
                name: "FK_Packages_Products_ProductId",
                table: "Packages");

            migrationBuilder.AddForeignKey(
                name: "FK_Barcodes_Packages_PackageId",
                table: "Barcodes",
                column: "PackageId",
                principalTable: "Packages",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDocuments_Orders_OrderId",
                table: "OrderDocuments",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Packages_Products_ProductId",
                table: "Packages",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id");
        }
    }
}
