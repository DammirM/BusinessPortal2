using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BusinessPortal2.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "personals",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    isAdmin = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_personals", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "leaveTypes",
                columns: table => new
                {
                    PersonalId = table.Column<int>(type: "int", nullable: false),
                    Vabb = table.Column<int>(type: "int", nullable: false),
                    Sick = table.Column<int>(type: "int", nullable: false),
                    Vacation = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_leaveTypes", x => x.PersonalId);
                    table.ForeignKey(
                        name: "FK_leaveTypes_personals_PersonalId",
                        column: x => x.PersonalId,
                        principalTable: "personals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "leaveTypes");

            migrationBuilder.DropTable(
                name: "personals");
        }
    }
}
