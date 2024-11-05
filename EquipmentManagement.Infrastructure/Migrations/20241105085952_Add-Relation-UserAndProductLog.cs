using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EquipmentManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddRelationUserAndProductLog : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_ProductLogs_UserId",
                table: "ProductLogs",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductLogs_Users_UserId",
                table: "ProductLogs",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductLogs_Users_UserId",
                table: "ProductLogs");

            migrationBuilder.DropIndex(
                name: "IX_ProductLogs_UserId",
                table: "ProductLogs");
        }
    }
}
