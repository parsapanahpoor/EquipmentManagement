using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EquipmentManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChnageTypeMealTypeMealPricing : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "MealType",
                table: "MealPricing",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "MealType",
                table: "MealPricing",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
