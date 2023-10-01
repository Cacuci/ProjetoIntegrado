using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Inbound.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddIndividualValueGeneratedEqualsNever : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Quantity",
                table: "OrderItems",
                type: "decimal(15,3)",
                precision: 15,
                scale: 3,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Quantity",
                table: "OrderItems",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(15,3)",
                oldPrecision: 15,
                oldScale: 3);
        }
    }
}
