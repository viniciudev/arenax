using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class altercollunimagesportcurt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "SportsCourt");

            migrationBuilder.AddColumn<string>(
                name: "UniqueFileName",
                table: "SportsCourt",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UniqueFileName",
                table: "SportsCourt");

            migrationBuilder.AddColumn<byte[]>(
                name: "Image",
                table: "SportsCourt",
                type: "bytea",
                nullable: true);
        }
    }
}
