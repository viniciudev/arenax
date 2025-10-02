using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addcolluninsportcenter : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Logo",
                table: "SportsCenter");

            migrationBuilder.AddColumn<string>(
                name: "UniqueFileName",
                table: "SportsCenter",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UniqueFileName",
                table: "SportsCenter");

            migrationBuilder.AddColumn<byte[]>(
                name: "Logo",
                table: "SportsCenter",
                type: "bytea",
                nullable: true);
        }
    }
}
