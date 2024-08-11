using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EquipmentManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddUserSelectedOrganizationChart1Table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OrganizationChartId",
                table: "UserSelectedOrganizationCharts");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "OrganizationChartId",
                table: "UserSelectedOrganizationCharts",
                type: "decimal(20,0)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
