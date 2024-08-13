using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EquipmentManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddOrganizationRequest2Table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RequestDecisionMakers_OrganizationCharts_OrganizationChartAggregateId",
                table: "RequestDecisionMakers");

            migrationBuilder.DropForeignKey(
                name: "FK_RequestDecisionMakers_OrganziationRequests_OrganziationRequestEntityId",
                table: "RequestDecisionMakers");

            migrationBuilder.DropIndex(
                name: "IX_RequestDecisionMakers_OrganizationChartAggregateId",
                table: "RequestDecisionMakers");

            migrationBuilder.DropIndex(
                name: "IX_RequestDecisionMakers_OrganziationRequestEntityId",
                table: "RequestDecisionMakers");

            migrationBuilder.DropColumn(
                name: "OrganizationChartAggregateId",
                table: "RequestDecisionMakers");

            migrationBuilder.DropColumn(
                name: "OrganziationRequestEntityId",
                table: "RequestDecisionMakers");

            migrationBuilder.CreateIndex(
                name: "IX_RequestDecisionMakers_OrganizationChartId",
                table: "RequestDecisionMakers",
                column: "OrganizationChartId");

            migrationBuilder.CreateIndex(
                name: "IX_RequestDecisionMakers_OrganziationRequestId",
                table: "RequestDecisionMakers",
                column: "OrganziationRequestId");

            migrationBuilder.AddForeignKey(
                name: "FK_RequestDecisionMakers_OrganizationCharts_OrganizationChartId",
                table: "RequestDecisionMakers",
                column: "OrganizationChartId",
                principalTable: "OrganizationCharts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RequestDecisionMakers_OrganziationRequests_OrganziationRequestId",
                table: "RequestDecisionMakers",
                column: "OrganziationRequestId",
                principalTable: "OrganziationRequests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RequestDecisionMakers_OrganizationCharts_OrganizationChartId",
                table: "RequestDecisionMakers");

            migrationBuilder.DropForeignKey(
                name: "FK_RequestDecisionMakers_OrganziationRequests_OrganziationRequestId",
                table: "RequestDecisionMakers");

            migrationBuilder.DropIndex(
                name: "IX_RequestDecisionMakers_OrganizationChartId",
                table: "RequestDecisionMakers");

            migrationBuilder.DropIndex(
                name: "IX_RequestDecisionMakers_OrganziationRequestId",
                table: "RequestDecisionMakers");

            migrationBuilder.AddColumn<decimal>(
                name: "OrganizationChartAggregateId",
                table: "RequestDecisionMakers",
                type: "decimal(20,0)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "OrganziationRequestEntityId",
                table: "RequestDecisionMakers",
                type: "decimal(20,0)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_RequestDecisionMakers_OrganizationChartAggregateId",
                table: "RequestDecisionMakers",
                column: "OrganizationChartAggregateId");

            migrationBuilder.CreateIndex(
                name: "IX_RequestDecisionMakers_OrganziationRequestEntityId",
                table: "RequestDecisionMakers",
                column: "OrganziationRequestEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_RequestDecisionMakers_OrganizationCharts_OrganizationChartAggregateId",
                table: "RequestDecisionMakers",
                column: "OrganizationChartAggregateId",
                principalTable: "OrganizationCharts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RequestDecisionMakers_OrganziationRequests_OrganziationRequestEntityId",
                table: "RequestDecisionMakers",
                column: "OrganziationRequestEntityId",
                principalTable: "OrganziationRequests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
