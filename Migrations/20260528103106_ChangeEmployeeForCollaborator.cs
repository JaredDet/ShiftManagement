using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShiftManagement.Api.Migrations
{
    /// <inheritdoc />
    public partial class ChangeEmployeeForCollaborator : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_employment_assignments_employee_EmployeeId",
                table: "employment_assignments");

            migrationBuilder.DropTable(
                name: "employee");

            migrationBuilder.RenameColumn(
                name: "EmployeeId",
                table: "employment_assignments",
                newName: "CollaboratorId");

            migrationBuilder.RenameIndex(
                name: "IX_employment_assignments_EmployeeId",
                table: "employment_assignments",
                newName: "IX_employment_assignments_CollaboratorId");

            migrationBuilder.CreateTable(
                name: "collaborators",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uuid", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_collaborators", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_employment_assignments_collaborators_CollaboratorId",
                table: "employment_assignments",
                column: "CollaboratorId",
                principalTable: "collaborators",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_employment_assignments_collaborators_CollaboratorId",
                table: "employment_assignments");

            migrationBuilder.DropTable(
                name: "collaborators");

            migrationBuilder.RenameColumn(
                name: "CollaboratorId",
                table: "employment_assignments",
                newName: "EmployeeId");

            migrationBuilder.RenameIndex(
                name: "IX_employment_assignments_CollaboratorId",
                table: "employment_assignments",
                newName: "IX_employment_assignments_EmployeeId");

            migrationBuilder.CreateTable(
                name: "employee",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_employee", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_employment_assignments_employee_EmployeeId",
                table: "employment_assignments",
                column: "EmployeeId",
                principalTable: "employee",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
