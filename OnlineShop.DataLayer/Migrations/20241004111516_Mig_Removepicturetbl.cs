using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineShop.DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class Mig_Removepicturetbl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductPictures_Pictures_ImageId",
                table: "ProductPictures");

            migrationBuilder.DropTable(
                name: "Pictures");

            migrationBuilder.DropIndex(
                name: "IX_ProductPictures_ImageId",
                table: "ProductPictures");

            migrationBuilder.DropColumn(
                name: "ImageId",
                table: "ProductPictures");

            migrationBuilder.AddColumn<string>(
                name: "PictureName",
                table: "ProductPictures",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PictureName",
                table: "ProductPictures");

            migrationBuilder.AddColumn<int>(
                name: "ImageId",
                table: "ProductPictures",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Pictures",
                columns: table => new
                {
                    PictureId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PictureName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pictures", x => x.PictureId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductPictures_ImageId",
                table: "ProductPictures",
                column: "ImageId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductPictures_Pictures_ImageId",
                table: "ProductPictures",
                column: "ImageId",
                principalTable: "Pictures",
                principalColumn: "PictureId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
