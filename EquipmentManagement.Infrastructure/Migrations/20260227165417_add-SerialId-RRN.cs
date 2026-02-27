using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EquipmentManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addSerialIdRRN : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RRN",
                table: "EmployeeTransaction",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SerialId",
                table: "EmployeeTransaction",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RRN",
                table: "EmployeeTransaction");

            migrationBuilder.DropColumn(
                name: "SerialId",
                table: "EmployeeTransaction");
        }
    }
}
