using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EquipmentManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddRepairRequestRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsNeedToOutSource",
                table: "ExpertVisitorOpinions");

            migrationBuilder.AddColumn<bool>(
                name: "IsNeedToOutSource",
                table: "RepairRequests",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_RepairRequests_EmployeeUserId",
                table: "RepairRequests",
                column: "EmployeeUserId");

            migrationBuilder.CreateIndex(
                name: "IX_RepairRequests_ProductId",
                table: "RepairRequests",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_RepairRequests_Products_ProductId",
                table: "RepairRequests",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RepairRequests_Users_EmployeeUserId",
                table: "RepairRequests",
                column: "EmployeeUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RepairRequests_Products_ProductId",
                table: "RepairRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_RepairRequests_Users_EmployeeUserId",
                table: "RepairRequests");

            migrationBuilder.DropIndex(
                name: "IX_RepairRequests_EmployeeUserId",
                table: "RepairRequests");

            migrationBuilder.DropIndex(
                name: "IX_RepairRequests_ProductId",
                table: "RepairRequests");

            migrationBuilder.DropColumn(
                name: "IsNeedToOutSource",
                table: "RepairRequests");

            migrationBuilder.AddColumn<bool>(
                name: "IsNeedToOutSource",
                table: "ExpertVisitorOpinions",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
