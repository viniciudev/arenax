using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class deletelinkclientsportcenterfk : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SportsCenterClients_SportsCenter_SportsCenterId",
                table: "Client");

            migrationBuilder.DropIndex(
                name: "IX_Client_SportsCenterId",
                table: "Client");

            migrationBuilder.DropColumn(
                name: "SportsCenterId",
                table: "Client");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SportsCenterId",
                table: "Client",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Client_SportsCenterId",
                table: "Client",
                column: "SportsCenterId");

            migrationBuilder.AddForeignKey(
                name: "FK_SportsCenterClients_SportsCenter_SportsCenterId",
                table: "Client",
                column: "SportsCenterId",
                principalTable: "SportsCenter",
                principalColumn: "Id");
        }
    }
}
