using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EquipmentManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addRFIdCanReceiveFoodForEmployee : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "CanReceiveFood",
                table: "Employees",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RFId",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CanReceiveFood",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "RFId",
                table: "Employees");
        }
    }
}
