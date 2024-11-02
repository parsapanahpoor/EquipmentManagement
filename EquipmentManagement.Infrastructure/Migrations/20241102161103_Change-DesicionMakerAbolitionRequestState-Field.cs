using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EquipmentManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangeDesicionMakerAbolitionRequestStateField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DecisionAbolitionRequestRespons",
                columns: table => new
                {
                    Id = table.Column<decimal>(type: "decimal(20,0)", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AbolitionRequestId = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
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
                    table.PrimaryKey("PK_DecisionAbolitionRequestRespons", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DecisionAbolitionRequestRespons");
        }
    }
}
