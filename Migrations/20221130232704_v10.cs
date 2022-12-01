using Microsoft.EntityFrameworkCore.Migrations;

namespace MushroomWebsite.Migrations
{
    public partial class v10 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EntryMushroom");

            migrationBuilder.CreateTable(
                name: "EntryMushrooms",
                columns: table => new
                {
                    EntryId = table.Column<int>(type: "int", nullable: false),
                    MushroomId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntryMushrooms", x => new { x.EntryId, x.MushroomId });
                    table.ForeignKey(
                        name: "FK_EntryMushrooms_Entries_EntryId",
                        column: x => x.EntryId,
                        principalTable: "Entries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EntryMushrooms_Mushrooms_MushroomId",
                        column: x => x.MushroomId,
                        principalTable: "Mushrooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EntryMushrooms_MushroomId",
                table: "EntryMushrooms",
                column: "MushroomId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EntryMushrooms");

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
                        name: "FK_EntryMushroom_Entries_EntriesId",
                        column: x => x.EntriesId,
                        principalTable: "Entries",
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
                name: "IX_EntryMushroom_MushroomsId",
                table: "EntryMushroom",
                column: "MushroomsId");
        }
    }
}
