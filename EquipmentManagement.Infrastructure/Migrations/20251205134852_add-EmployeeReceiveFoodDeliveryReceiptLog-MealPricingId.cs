using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EquipmentManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addEmployeeReceiveFoodDeliveryReceiptLogMealPricingId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "MealPricingId",
                table: "EmployeeReceiveFoodDeliveryReceiptLogs",
                type: "decimal(20,0)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeReceiveFoodDeliveryReceiptLogs_MealPricingId",
                table: "EmployeeReceiveFoodDeliveryReceiptLogs",
                column: "MealPricingId");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeReceiveFoodDeliveryReceiptLogs_MealPricing_MealPricingId",
                table: "EmployeeReceiveFoodDeliveryReceiptLogs",
                column: "MealPricingId",
                principalTable: "MealPricing",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeReceiveFoodDeliveryReceiptLogs_MealPricing_MealPricingId",
                table: "EmployeeReceiveFoodDeliveryReceiptLogs");

            migrationBuilder.DropIndex(
                name: "IX_EmployeeReceiveFoodDeliveryReceiptLogs_MealPricingId",
                table: "EmployeeReceiveFoodDeliveryReceiptLogs");

            migrationBuilder.DropColumn(
                name: "MealPricingId",
                table: "EmployeeReceiveFoodDeliveryReceiptLogs");
        }
    }
}
