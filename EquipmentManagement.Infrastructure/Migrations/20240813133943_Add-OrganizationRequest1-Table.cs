using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EquipmentManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddOrganizationRequest1Table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RequestDecisionMaker_OrganizationCharts_OrganizationChartAggregateId",
                table: "RequestDecisionMaker");

            migrationBuilder.DropForeignKey(
                name: "FK_RequestDecisionMaker_OrganziationRequestEntity_OrganziationRequestEntityId",
                table: "RequestDecisionMaker");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RequestDecisionMaker",
                table: "RequestDecisionMaker");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrganziationRequestEntity",
                table: "OrganziationRequestEntity");

            migrationBuilder.RenameTable(
                name: "RequestDecisionMaker",
                newName: "RequestDecisionMakers");

            migrationBuilder.RenameTable(
                name: "OrganziationRequestEntity",
                newName: "OrganziationRequests");

            migrationBuilder.RenameIndex(
                name: "IX_RequestDecisionMaker_OrganziationRequestEntityId",
                table: "RequestDecisionMakers",
                newName: "IX_RequestDecisionMakers_OrganziationRequestEntityId");

            migrationBuilder.RenameIndex(
                name: "IX_RequestDecisionMaker_OrganizationChartAggregateId",
                table: "RequestDecisionMakers",
                newName: "IX_RequestDecisionMakers_OrganizationChartAggregateId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RequestDecisionMakers",
                table: "RequestDecisionMakers",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrganziationRequests",
                table: "OrganziationRequests",
                column: "Id");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RequestDecisionMakers_OrganizationCharts_OrganizationChartAggregateId",
                table: "RequestDecisionMakers");

            migrationBuilder.DropForeignKey(
                name: "FK_RequestDecisionMakers_OrganziationRequests_OrganziationRequestEntityId",
                table: "RequestDecisionMakers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RequestDecisionMakers",
                table: "RequestDecisionMakers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrganziationRequests",
                table: "OrganziationRequests");

            migrationBuilder.RenameTable(
                name: "RequestDecisionMakers",
                newName: "RequestDecisionMaker");

            migrationBuilder.RenameTable(
                name: "OrganziationRequests",
                newName: "OrganziationRequestEntity");

            migrationBuilder.RenameIndex(
                name: "IX_RequestDecisionMakers_OrganziationRequestEntityId",
                table: "RequestDecisionMaker",
                newName: "IX_RequestDecisionMaker_OrganziationRequestEntityId");

            migrationBuilder.RenameIndex(
                name: "IX_RequestDecisionMakers_OrganizationChartAggregateId",
                table: "RequestDecisionMaker",
                newName: "IX_RequestDecisionMaker_OrganizationChartAggregateId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RequestDecisionMaker",
                table: "RequestDecisionMaker",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrganziationRequestEntity",
                table: "OrganziationRequestEntity",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RequestDecisionMaker_OrganizationCharts_OrganizationChartAggregateId",
                table: "RequestDecisionMaker",
                column: "OrganizationChartAggregateId",
                principalTable: "OrganizationCharts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RequestDecisionMaker_OrganziationRequestEntity_OrganziationRequestEntityId",
                table: "RequestDecisionMaker",
                column: "OrganziationRequestEntityId",
                principalTable: "OrganziationRequestEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
