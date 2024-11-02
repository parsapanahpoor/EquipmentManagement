using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EquipmentManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddAbolitionRequestTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AbolitionRequests",
                columns: table => new
                {
                    Id = table.Column<decimal>(type: "decimal(20,0)", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeUserId = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    ProductId = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    ExpertVisitorRepairRequestState = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsFinally = table.Column<bool>(type: "bit", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AbolitionRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AbolitionRequests_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AbolitionRequests_Users_EmployeeUserId",
                        column: x => x.EmployeeUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ExpertVisitorOpinionForAbolitionRequestEntities",
                columns: table => new
                {
                    Id = table.Column<decimal>(type: "decimal(20,0)", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AbolitionRequestId = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    ExpertUserId = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    ResponsType = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExpertVisitorOpinionForAbolitionRequestEntities", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AbolitionRequests_EmployeeUserId",
                table: "AbolitionRequests",
                column: "EmployeeUserId");

            migrationBuilder.CreateIndex(
                name: "IX_AbolitionRequests_ProductId",
                table: "AbolitionRequests",
                column: "ProductId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AbolitionRequests");

            migrationBuilder.DropTable(
                name: "ExpertVisitorOpinionForAbolitionRequestEntities");
        }
    }
}
