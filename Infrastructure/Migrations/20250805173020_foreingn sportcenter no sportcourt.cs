using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class foreingnsportcenternosportcourt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IdSportsCenter",
                table: "SportsCourt",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SportsCourt_IdSportsCenter",
                table: "SportsCourt",
                column: "IdSportsCenter");

            migrationBuilder.AddForeignKey(
                name: "FK_SportsCourt_SportsCenter_IdSportsCenter",
                table: "SportsCourt",
                column: "IdSportsCenter",
                principalTable: "SportsCenter",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SportsCourt_SportsCenter_IdSportsCenter",
                table: "SportsCourt");

            migrationBuilder.DropIndex(
                name: "IX_SportsCourt_IdSportsCenter",
                table: "SportsCourt");

            migrationBuilder.DropColumn(
                name: "IdSportsCenter",
                table: "SportsCourt");
        }
    }
}
