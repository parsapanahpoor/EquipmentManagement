using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EquipmentManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddMealPricingIdEmployeeShiftMealSelected : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Meal",
                table: "EmployeeShiftMealFSelected");

            migrationBuilder.AddColumn<decimal>(
                name: "MealPricingId",
                table: "EmployeeShiftMealFSelected",
                type: "decimal(20,0)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateIndex(
                name: "IX_UserRole_RoleId",
                table: "UserRole",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRole_UserId",
                table: "UserRole",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeShiftMealFSelected_MealPricingId",
                table: "EmployeeShiftMealFSelected",
                column: "MealPricingId");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeShiftMealFSelected_MealPricing_MealPricingId",
                table: "EmployeeShiftMealFSelected",
                column: "MealPricingId",
                principalTable: "MealPricing",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRole_Role_RoleId",
                table: "UserRole",
                column: "RoleId",
                principalTable: "Role",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRole_Users_UserId",
                table: "UserRole",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeShiftMealFSelected_MealPricing_MealPricingId",
                table: "EmployeeShiftMealFSelected");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRole_Role_RoleId",
                table: "UserRole");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRole_Users_UserId",
                table: "UserRole");

            migrationBuilder.DropIndex(
                name: "IX_UserRole_RoleId",
                table: "UserRole");

            migrationBuilder.DropIndex(
                name: "IX_UserRole_UserId",
                table: "UserRole");

            migrationBuilder.DropIndex(
                name: "IX_EmployeeShiftMealFSelected_MealPricingId",
                table: "EmployeeShiftMealFSelected");

            migrationBuilder.DropColumn(
                name: "MealPricingId",
                table: "EmployeeShiftMealFSelected");

            migrationBuilder.AddColumn<int>(
                name: "Meal",
                table: "EmployeeShiftMealFSelected",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
