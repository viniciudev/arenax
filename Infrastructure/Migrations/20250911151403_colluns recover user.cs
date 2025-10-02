using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class collunsrecoveruser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PasswordResetToken",
                table: "User",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "PasswordResetTokenExpiry",
                table: "User",
                type: "timestamp without time zone",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PasswordResetToken",
                table: "User");

            migrationBuilder.DropColumn(
                name: "PasswordResetTokenExpiry",
                table: "User");
        }
    }
}
