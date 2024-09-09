using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineShop.DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class Mig_Addavatartousertbl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserAvatar",
                table: "Users",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserAvatar",
                table: "Users");
        }
    }
}
