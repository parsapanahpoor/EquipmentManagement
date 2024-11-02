using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EquipmentManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangeExpertAbolitionRequestStateField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsFinally",
                table: "AbolitionRequests");

            migrationBuilder.AddColumn<int>(
                name: "RequestState",
                table: "AbolitionRequests",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RequestState",
                table: "AbolitionRequests");

            migrationBuilder.AddColumn<bool>(
                name: "IsFinally",
                table: "AbolitionRequests",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
