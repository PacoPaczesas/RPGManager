using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RPGManager.Migrations
{
    /// <inheritdoc />
    public partial class bazaPoDodaniuCurrentHP : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CurrentHP",
                table: "NPCs",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CurrentHP",
                table: "NPCs");
        }
    }
}
