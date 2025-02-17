using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace crypto.Migrations
{
    /// <inheritdoc />
    public partial class AddRefreshTokenFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Add 'RefreshToken' column
            migrationBuilder.AddColumn<string>(
                name: "RefreshToken",
                table: "Users",
                type: "text",
                nullable: true);

            // Add 'RefreshTokenExpiryTime' column
            migrationBuilder.AddColumn<DateTime>(
                name: "RefreshTokenExpiryTime",
                table: "Users",
                type: "timestamp with time zone",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Drop 'RefreshToken' column
            migrationBuilder.DropColumn(
                name: "RefreshToken",
                table: "Users");

            // Drop 'RefreshTokenExpiryTime' column
            migrationBuilder.DropColumn(
                name: "RefreshTokenExpiryTime",
                table: "Users");
        }
    }
}
