﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace crypto.Migrations
{
    /// <inheritdoc />
    public partial class RemoveJWTColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "JWT",
                table: "Users");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateCreated",
                table: "Users",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateCreated",
                table: "Users");

            migrationBuilder.AddColumn<string>(
                name: "JWT",
                table: "Users",
                type: "text",
                nullable: true);
        }
    }
}
