using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class deletelinkclientsportcenter : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SportsCenterClients_SportsCenter_IdSportCenter",
                table: "Client");

            migrationBuilder.RenameColumn(
                name: "IdSportCenter",
                table: "Client",
                newName: "SportsCenterId");

            migrationBuilder.RenameIndex(
                name: "IX_Client_IdSportCenter",
                table: "Client",
                newName: "IX_Client_SportsCenterId");

            migrationBuilder.AddForeignKey(
                name: "FK_SportsCenterClients_SportsCenter_SportsCenterId",
                table: "Client",
                column: "SportsCenterId",
                principalTable: "SportsCenter",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SportsCenterClients_SportsCenter_SportsCenterId",
                table: "Client");

            migrationBuilder.RenameColumn(
                name: "SportsCenterId",
                table: "Client",
                newName: "IdSportCenter");

            migrationBuilder.RenameIndex(
                name: "IX_Client_SportsCenterId",
                table: "Client",
                newName: "IX_Client_IdSportCenter");

            migrationBuilder.AddForeignKey(
                name: "FK_SportsCenterClients_SportsCenter_IdSportCenter",
                table: "Client",
                column: "IdSportCenter",
                principalTable: "SportsCenter",
                principalColumn: "Id");
        }
    }
}
