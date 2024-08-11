using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EquipmentManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangeOrganizationChartTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OrganizationDutyId",
                table: "OrganizationCharts");

            migrationBuilder.DropColumn(
                name: "ParentPath",
                table: "OrganizationCharts");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "OrganizationDutyId",
                table: "OrganizationCharts",
                type: "decimal(20,0)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ParentPath",
                table: "OrganizationCharts",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
