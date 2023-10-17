using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BusinessPortal2.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LeaveRequest_personals_PersonalId",
                table: "LeaveRequest");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LeaveRequest",
                table: "LeaveRequest");

            migrationBuilder.RenameTable(
                name: "LeaveRequest",
                newName: "leaveRequests");

            migrationBuilder.RenameIndex(
                name: "IX_LeaveRequest_PersonalId",
                table: "leaveRequests",
                newName: "IX_leaveRequests_PersonalId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_leaveRequests",
                table: "leaveRequests",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_leaveRequests_personals_PersonalId",
                table: "leaveRequests",
                column: "PersonalId",
                principalTable: "personals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_leaveRequests_personals_PersonalId",
                table: "leaveRequests");

            migrationBuilder.DropPrimaryKey(
                name: "PK_leaveRequests",
                table: "leaveRequests");

            migrationBuilder.RenameTable(
                name: "leaveRequests",
                newName: "LeaveRequest");

            migrationBuilder.RenameIndex(
                name: "IX_leaveRequests_PersonalId",
                table: "LeaveRequest",
                newName: "IX_LeaveRequest_PersonalId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LeaveRequest",
                table: "LeaveRequest",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_LeaveRequest_personals_PersonalId",
                table: "LeaveRequest",
                column: "PersonalId",
                principalTable: "personals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
