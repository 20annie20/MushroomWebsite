using Microsoft.EntityFrameworkCore.Migrations;

namespace MushroomWebsite.Migrations
{
    public partial class v8 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Articles_Entry_EntryId",
                table: "Articles");

            migrationBuilder.DropForeignKey(
                name: "FK_Entry_Users_UserId",
                table: "Entry");

            migrationBuilder.DropForeignKey(
                name: "FK_EntryMushroom_Entry_EntriesId",
                table: "EntryMushroom");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Entry",
                table: "Entry");

            migrationBuilder.RenameTable(
                name: "Entry",
                newName: "Entries");

            migrationBuilder.RenameIndex(
                name: "IX_Entry_UserId",
                table: "Entries",
                newName: "IX_Entries_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Entries",
                table: "Entries",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Articles_Entries_EntryId",
                table: "Articles",
                column: "EntryId",
                principalTable: "Entries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Entries_Users_UserId",
                table: "Entries",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EntryMushroom_Entries_EntriesId",
                table: "EntryMushroom",
                column: "EntriesId",
                principalTable: "Entries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Articles_Entries_EntryId",
                table: "Articles");

            migrationBuilder.DropForeignKey(
                name: "FK_Entries_Users_UserId",
                table: "Entries");

            migrationBuilder.DropForeignKey(
                name: "FK_EntryMushroom_Entries_EntriesId",
                table: "EntryMushroom");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Entries",
                table: "Entries");

            migrationBuilder.RenameTable(
                name: "Entries",
                newName: "Entry");

            migrationBuilder.RenameIndex(
                name: "IX_Entries_UserId",
                table: "Entry",
                newName: "IX_Entry_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Entry",
                table: "Entry",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Articles_Entry_EntryId",
                table: "Articles",
                column: "EntryId",
                principalTable: "Entry",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Entry_Users_UserId",
                table: "Entry",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EntryMushroom_Entry_EntriesId",
                table: "EntryMushroom",
                column: "EntriesId",
                principalTable: "Entry",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
