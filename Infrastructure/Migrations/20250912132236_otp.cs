using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class otp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PasswordResetToken",
                table: "User");

            migrationBuilder.DropColumn(
                name: "PasswordResetTokenExpiry",
                table: "User");

            migrationBuilder.CreateTable(
                name: "Otp",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Email = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    HashedCode = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    Purpose = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    ExpiryDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Attempts = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Otp", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Otp");

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
    }
}
