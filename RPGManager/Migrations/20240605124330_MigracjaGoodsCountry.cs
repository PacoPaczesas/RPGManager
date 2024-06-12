using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RPGManager.Migrations
{
    /// <inheritdoc />
    public partial class MigracjaGoodsCountry : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Goods_Countries_CountryId",
                table: "Goods");

            migrationBuilder.DropIndex(
                name: "IX_Goods_CountryId",
                table: "Goods");

            migrationBuilder.DropColumn(
                name: "CountryId",
                table: "Goods");

            migrationBuilder.CreateTable(
                name: "CountryGoods",
                columns: table => new
                {
                    CountryId = table.Column<int>(type: "int", nullable: false),
                    GoodsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CountryGoods", x => new { x.CountryId, x.GoodsId });
                    table.ForeignKey(
                        name: "FK_CountryGoods_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CountryGoods_Goods_GoodsId",
                        column: x => x.GoodsId,
                        principalTable: "Goods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CountryGoods_GoodsId",
                table: "CountryGoods",
                column: "GoodsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CountryGoods");

            migrationBuilder.AddColumn<int>(
                name: "CountryId",
                table: "Goods",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Goods_CountryId",
                table: "Goods",
                column: "CountryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Goods_Countries_CountryId",
                table: "Goods",
                column: "CountryId",
                principalTable: "Countries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
