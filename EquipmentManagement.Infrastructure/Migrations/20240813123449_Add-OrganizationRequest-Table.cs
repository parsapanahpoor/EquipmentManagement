using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EquipmentManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddOrganizationRequestTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OrganziationRequestEntity",
                columns: table => new
                {
                    Id = table.Column<decimal>(type: "decimal(20,0)", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RequestType = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrganziationRequestEntity", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RequestDecisionMaker",
                columns: table => new
                {
                    Id = table.Column<decimal>(type: "decimal(20,0)", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrganziationRequestEntityId = table.Column<decimal>(type: "decimal(20,0)", nullable: true),
                    OrganziationRequestId = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    OrganizationChartAggregateId = table.Column<decimal>(type: "decimal(20,0)", nullable: true),
                    OrganizationChartId = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestDecisionMaker", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RequestDecisionMaker_OrganizationCharts_OrganizationChartAggregateId",
                        column: x => x.OrganizationChartAggregateId,
                        principalTable: "OrganizationCharts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RequestDecisionMaker_OrganziationRequestEntity_OrganziationRequestEntityId",
                        column: x => x.OrganziationRequestEntityId,
                        principalTable: "OrganziationRequestEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RequestDecisionMaker_OrganizationChartAggregateId",
                table: "RequestDecisionMaker",
                column: "OrganizationChartAggregateId");

            migrationBuilder.CreateIndex(
                name: "IX_RequestDecisionMaker_OrganziationRequestEntityId",
                table: "RequestDecisionMaker",
                column: "OrganziationRequestEntityId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RequestDecisionMaker");

            migrationBuilder.DropTable(
                name: "OrganziationRequestEntity");
        }
    }
}
