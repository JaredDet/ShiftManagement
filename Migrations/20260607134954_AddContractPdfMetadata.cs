using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShiftManagement.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddContractPdfMetadata : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_employment_assignments_positions_PositionId",
                table: "employment_assignments");

            migrationBuilder.DropIndex(
                name: "IX_employment_assignments_PositionId",
                table: "employment_assignments");

            migrationBuilder.DropColumn(
                name: "PositionId",
                table: "employment_assignments");

            migrationBuilder.RenameColumn(
                name: "CanceledAt",
                table: "claims",
                newName: "CancelledAt");

            migrationBuilder.CreateTable(
                name: "contract_terminations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ContractId = table.Column<Guid>(type: "uuid", nullable: false),
                    Reason = table.Column<int>(type: "integer", nullable: false),
                    Comment = table.Column<string>(type: "text", nullable: true),
                    TerminatedByUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    SettlementDocumentUrl = table.Column<string>(type: "text", nullable: true),
                    TerminationDate = table.Column<DateOnly>(type: "date", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_contract_terminations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "employment_contracts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CollaboratorId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedByUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    WorkScheduleType = table.Column<int>(type: "integer", nullable: false),
                    SalaryAmount = table.Column<decimal>(type: "numeric", nullable: false),
                    Currency = table.Column<int>(type: "integer", nullable: false),
                    PayPeriod = table.Column<int>(type: "integer", nullable: false),
                    StartDate = table.Column<DateOnly>(type: "date", nullable: false),
                    EndDate = table.Column<DateOnly>(type: "date", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    TerminationId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_employment_contracts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_employment_contracts_contract_terminations_TerminationId",
                        column: x => x.TerminationId,
                        principalTable: "contract_terminations",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_contract_terminations_ContractId",
                table: "contract_terminations",
                column: "ContractId");

            migrationBuilder.CreateIndex(
                name: "IX_employment_contracts_CollaboratorId",
                table: "employment_contracts",
                column: "CollaboratorId");

            migrationBuilder.CreateIndex(
                name: "IX_employment_contracts_TerminationId",
                table: "employment_contracts",
                column: "TerminationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "employment_contracts");

            migrationBuilder.DropTable(
                name: "contract_terminations");

            migrationBuilder.RenameColumn(
                name: "CancelledAt",
                table: "claims",
                newName: "CanceledAt");

            migrationBuilder.AddColumn<Guid>(
                name: "PositionId",
                table: "employment_assignments",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_employment_assignments_PositionId",
                table: "employment_assignments",
                column: "PositionId");

            migrationBuilder.AddForeignKey(
                name: "FK_employment_assignments_positions_PositionId",
                table: "employment_assignments",
                column: "PositionId",
                principalTable: "positions",
                principalColumn: "Id");
        }
    }
}
