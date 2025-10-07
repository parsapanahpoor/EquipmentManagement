using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EquipmentManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddEmployeeRecevieFoodLogTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "employeeReceiveFoodDeliveryReceiptLogs",
                columns: table => new
                {
                    Id = table.Column<decimal>(type: "decimal(20,0)", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeId = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_employeeReceiveFoodDeliveryReceiptLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_employeeReceiveFoodDeliveryReceiptLogs_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_employeeReceiveFoodDeliveryReceiptLogs_EmployeeId",
                table: "employeeReceiveFoodDeliveryReceiptLogs",
                column: "EmployeeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "employeeReceiveFoodDeliveryReceiptLogs");
        }
    }
}
