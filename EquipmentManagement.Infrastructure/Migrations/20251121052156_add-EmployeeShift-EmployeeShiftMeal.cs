using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EquipmentManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addEmployeeShiftEmployeeShiftMeal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EmployeeShiftSelected",
                columns: table => new
                {
                    Id = table.Column<decimal>(type: "decimal(20,0)", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateOnly>(type: "date", nullable: false),
                    EmployeeId = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeShiftSelected", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployeeShiftSelected_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeShiftMealFSelected",
                columns: table => new
                {
                    Id = table.Column<decimal>(type: "decimal(20,0)", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Meal = table.Column<int>(type: "int", nullable: false),
                    EmployeeShiftSelectedId = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeShiftMealFSelected", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployeeShiftMealFSelected_EmployeeShiftSelected_EmployeeShiftSelectedId",
                        column: x => x.EmployeeShiftSelectedId,
                        principalTable: "EmployeeShiftSelected",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeShiftMealFSelected_EmployeeShiftSelectedId",
                table: "EmployeeShiftMealFSelected",
                column: "EmployeeShiftSelectedId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeShiftSelected_EmployeeId",
                table: "EmployeeShiftSelected",
                column: "EmployeeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmployeeShiftMealFSelected");

            migrationBuilder.DropTable(
                name: "EmployeeShiftSelected");
        }
    }
}
