using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EquipmentManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangeNameEmployeeRecevieFoodLogTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_employeeReceiveFoodDeliveryReceiptLogs_Employees_EmployeeId",
                table: "employeeReceiveFoodDeliveryReceiptLogs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_employeeReceiveFoodDeliveryReceiptLogs",
                table: "employeeReceiveFoodDeliveryReceiptLogs");

            migrationBuilder.RenameTable(
                name: "employeeReceiveFoodDeliveryReceiptLogs",
                newName: "EmployeeReceiveFoodDeliveryReceiptLogs");

            migrationBuilder.RenameIndex(
                name: "IX_employeeReceiveFoodDeliveryReceiptLogs_EmployeeId",
                table: "EmployeeReceiveFoodDeliveryReceiptLogs",
                newName: "IX_EmployeeReceiveFoodDeliveryReceiptLogs_EmployeeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EmployeeReceiveFoodDeliveryReceiptLogs",
                table: "EmployeeReceiveFoodDeliveryReceiptLogs",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeReceiveFoodDeliveryReceiptLogs_Employees_EmployeeId",
                table: "EmployeeReceiveFoodDeliveryReceiptLogs",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeReceiveFoodDeliveryReceiptLogs_Employees_EmployeeId",
                table: "EmployeeReceiveFoodDeliveryReceiptLogs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EmployeeReceiveFoodDeliveryReceiptLogs",
                table: "EmployeeReceiveFoodDeliveryReceiptLogs");

            migrationBuilder.RenameTable(
                name: "EmployeeReceiveFoodDeliveryReceiptLogs",
                newName: "employeeReceiveFoodDeliveryReceiptLogs");

            migrationBuilder.RenameIndex(
                name: "IX_EmployeeReceiveFoodDeliveryReceiptLogs_EmployeeId",
                table: "employeeReceiveFoodDeliveryReceiptLogs",
                newName: "IX_employeeReceiveFoodDeliveryReceiptLogs_EmployeeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_employeeReceiveFoodDeliveryReceiptLogs",
                table: "employeeReceiveFoodDeliveryReceiptLogs",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_employeeReceiveFoodDeliveryReceiptLogs_Employees_EmployeeId",
                table: "employeeReceiveFoodDeliveryReceiptLogs",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
