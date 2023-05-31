using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NawigacjaSklepowaAPI.Migrations
{
    /// <inheritdoc />
    public partial class ShopRating : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Rating",
                table: "Shops",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RatingCount",
                table: "Shops",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Rating",
                table: "Shops");

            migrationBuilder.DropColumn(
                name: "RatingCount",
                table: "Shops");
        }
    }
}
