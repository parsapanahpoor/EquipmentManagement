using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EquipmentManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddExpertVisitorOpinionEntity1Table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RepairRequestState",
                table: "RepairRequests",
                newName: "ExpertVisitorRepairRequestState");

            migrationBuilder.AddColumn<int>(
                name: "DesicionMakersRepairRequestState",
                table: "RepairRequests",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DesicionMakersRepairRequestState",
                table: "RepairRequests");

            migrationBuilder.RenameColumn(
                name: "ExpertVisitorRepairRequestState",
                table: "RepairRequests",
                newName: "RepairRequestState");
        }
    }
}
