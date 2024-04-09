using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RPGManager.Migrations
{
    /// <inheritdoc />
    public partial class UpdateNPCWithNewProperties2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Sila",
                table: "NPCs",
                newName: "Strength");

            migrationBuilder.RenameColumn(
                name: "Moc",
                table: "NPCs",
                newName: "Might");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Strength",
                table: "NPCs",
                newName: "Sila");

            migrationBuilder.RenameColumn(
                name: "Might",
                table: "NPCs",
                newName: "Moc");
        }
    }
}
