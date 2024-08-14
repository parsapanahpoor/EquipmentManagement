using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EquipmentManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddRepairRequestsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DecisionRepairRequestRespons",
                columns: table => new
                {
                    Id = table.Column<decimal>(type: "decimal(20,0)", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrganizationChartId = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    EmployeeUserId = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    RejectDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Response = table.Column<int>(type: "int", nullable: false),
                    Sign = table.Column<int>(type: "int", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DecisionRepairRequestRespons", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RepairRequests",
                columns: table => new
                {
                    Id = table.Column<decimal>(type: "decimal(20,0)", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeUserId = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    ProductId = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    RepairRequestState = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RepairRequests", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DecisionRepairRequestRespons");

            migrationBuilder.DropTable(
                name: "RepairRequests");
        }
    }
}
