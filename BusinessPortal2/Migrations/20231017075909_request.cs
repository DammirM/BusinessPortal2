using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BusinessPortal2.Migrations
{
    /// <inheritdoc />
    public partial class request : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LeaveRequest",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateRequest = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ApprovalState = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LeaveTypeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PersonalId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeaveRequest", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LeaveRequest_personals_PersonalId",
                        column: x => x.PersonalId,
                        principalTable: "personals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LeaveRequest_PersonalId",
                table: "LeaveRequest",
                column: "PersonalId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LeaveRequest");
        }
    }
}
