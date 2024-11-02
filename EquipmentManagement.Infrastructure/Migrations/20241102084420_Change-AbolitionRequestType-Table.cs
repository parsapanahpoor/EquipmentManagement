using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EquipmentManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangeAbolitionRequestTypeTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ExpertVisitorRepairRequestState",
                table: "AbolitionRequests",
                newName: "ExpertVisitorAbolitionRequestState");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ExpertVisitorAbolitionRequestState",
                table: "AbolitionRequests",
                newName: "ExpertVisitorRepairRequestState");
        }
    }
}
