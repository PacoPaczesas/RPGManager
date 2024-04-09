using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RPGManager.Migrations
{
    /// <inheritdoc />
    public partial class UpdateNPCWithNewProperties : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AC",
                table: "NPCs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Exp",
                table: "NPCs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "HP",
                table: "NPCs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Moc",
                table: "NPCs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Sila",
                table: "NPCs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "NPCId",
                table: "Notes",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AC",
                table: "NPCs");

            migrationBuilder.DropColumn(
                name: "Exp",
                table: "NPCs");

            migrationBuilder.DropColumn(
                name: "HP",
                table: "NPCs");

            migrationBuilder.DropColumn(
                name: "Moc",
                table: "NPCs");

            migrationBuilder.DropColumn(
                name: "Sila",
                table: "NPCs");

            migrationBuilder.AlterColumn<int>(
                name: "NPCId",
                table: "Notes",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
