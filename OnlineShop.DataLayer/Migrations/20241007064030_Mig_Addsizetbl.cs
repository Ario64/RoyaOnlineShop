﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineShop.DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class Mig_Addsizetbl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Sizes",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Sizes");
        }
    }
}