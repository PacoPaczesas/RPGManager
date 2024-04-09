using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RPGManager.Migrations
{
    /// <inheritdoc />
    public partial class UpdateNPCWithNewProperties4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Lvl",
                table: "NPCs",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Lvl",
                table: "NPCs");
        }
    }
}
