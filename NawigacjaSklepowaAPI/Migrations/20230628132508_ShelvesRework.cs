using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NawigacjaSklepowaAPI.Migrations
{
    /// <inheritdoc />
    public partial class ShelvesRework : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Shops_ShopId",
                table: "Products");

            migrationBuilder.DropTable(
                name: "Layouts");

            migrationBuilder.DropColumn(
                name: "Floor",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Shelves",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "ShopId",
                table: "Products",
                newName: "ShelfId");

            migrationBuilder.RenameIndex(
                name: "IX_Products_ShopId",
                table: "Products",
                newName: "IX_Products_ShelfId");

            migrationBuilder.CreateTable(
                name: "Shelves",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Color = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Height = table.Column<int>(type: "int", nullable: false),
                    Width = table.Column<int>(type: "int", nullable: false),
                    X = table.Column<int>(type: "int", nullable: false),
                    Y = table.Column<int>(type: "int", nullable: false),
                    ShopId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shelves", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Shelves_Shops_ShopId",
                        column: x => x.ShopId,
                        principalTable: "Shops",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Shelves_ShopId",
                table: "Shelves",
                column: "ShopId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Shelves_ShelfId",
                table: "Products",
                column: "ShelfId",
                principalTable: "Shelves",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Shelves_ShelfId",
                table: "Products");

            migrationBuilder.DropTable(
                name: "Shelves");

            migrationBuilder.RenameColumn(
                name: "ShelfId",
                table: "Products",
                newName: "ShopId");

            migrationBuilder.RenameIndex(
                name: "IX_Products_ShelfId",
                table: "Products",
                newName: "IX_Products_ShopId");

            migrationBuilder.AddColumn<int>(
                name: "Floor",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Shelves",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Layouts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ShopId = table.Column<int>(type: "int", nullable: false),
                    Canvas = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Layouts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Layouts_Shops_ShopId",
                        column: x => x.ShopId,
                        principalTable: "Shops",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Layouts_ShopId",
                table: "Layouts",
                column: "ShopId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Shops_ShopId",
                table: "Products",
                column: "ShopId",
                principalTable: "Shops",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
