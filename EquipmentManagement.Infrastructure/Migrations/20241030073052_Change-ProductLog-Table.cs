using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EquipmentManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangeProductLogTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_ProductLogs_PlaceId",
                table: "ProductLogs",
                column: "PlaceId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductLogs_ProductId",
                table: "ProductLogs",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductLogs_Places_PlaceId",
                table: "ProductLogs",
                column: "PlaceId",
                principalTable: "Places",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductLogs_Products_ProductId",
                table: "ProductLogs",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductLogs_Places_PlaceId",
                table: "ProductLogs");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductLogs_Products_ProductId",
                table: "ProductLogs");

            migrationBuilder.DropIndex(
                name: "IX_ProductLogs_PlaceId",
                table: "ProductLogs");

            migrationBuilder.DropIndex(
                name: "IX_ProductLogs_ProductId",
                table: "ProductLogs");
        }
    }
}
