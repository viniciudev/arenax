using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class appointmentslinkclients : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IdSportsCenterClient",
                table: "SportsCourtAppointments",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SportsCourtAppointments_IdSportsCenterClient",
                table: "SportsCourtAppointments",
                column: "IdSportsCenterClient");

            migrationBuilder.AddForeignKey(
                name: "FK_SportsCourtAppointments_SportsCenterClients_IdSportsCenterC~",
                table: "SportsCourtAppointments",
                column: "IdSportsCenterClient",
                principalTable: "SportsCenterClients",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SportsCourtAppointments_SportsCenterClients_IdSportsCenterC~",
                table: "SportsCourtAppointments");

            migrationBuilder.DropIndex(
                name: "IX_SportsCourtAppointments_IdSportsCenterClient",
                table: "SportsCourtAppointments");

            migrationBuilder.DropColumn(
                name: "IdSportsCenterClient",
                table: "SportsCourtAppointments");
        }
    }
}
