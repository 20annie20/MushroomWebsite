using Microsoft.EntityFrameworkCore.Migrations;

namespace MushroomWebsite.Migrations
{
    public partial class v7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EntryId",
                table: "Articles",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Entry",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Entry", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Entry_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EntryMushroom",
                columns: table => new
                {
                    EntriesId = table.Column<int>(type: "int", nullable: false),
                    MushroomsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntryMushroom", x => new { x.EntriesId, x.MushroomsId });
                    table.ForeignKey(
                        name: "FK_EntryMushroom_Entry_EntriesId",
                        column: x => x.EntriesId,
                        principalTable: "Entry",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EntryMushroom_Mushrooms_MushroomsId",
                        column: x => x.MushroomsId,
                        principalTable: "Mushrooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Articles_EntryId",
                table: "Articles",
                column: "EntryId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Entry_UserId",
                table: "Entry",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_EntryMushroom_MushroomsId",
                table: "EntryMushroom",
                column: "MushroomsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Articles_Entry_EntryId",
                table: "Articles",
                column: "EntryId",
                principalTable: "Entry",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Articles_Entry_EntryId",
                table: "Articles");

            migrationBuilder.DropTable(
                name: "EntryMushroom");

            migrationBuilder.DropTable(
                name: "Entry");

            migrationBuilder.DropIndex(
                name: "IX_Articles_EntryId",
                table: "Articles");

            migrationBuilder.DropColumn(
                name: "EntryId",
                table: "Articles");
        }
    }
}
