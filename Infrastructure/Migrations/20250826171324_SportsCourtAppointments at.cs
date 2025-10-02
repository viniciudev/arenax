using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SportsCourtAppointmentsat : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Deletar a foreign key
            migrationBuilder.DropForeignKey(
                name: "FK_SportsCourtAppointments_SportsCenterClients_IdSportsCenterC~",
                table: "SportsCourtAppointments");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Recriar a foreign key no método Down (para rollback)
            migrationBuilder.AddForeignKey(
                name: "FK_SportsCourtAppointments_SportsCenterClients_IdSportsCenterC~",
                table: "SportsCourtAppointments",
                column: "IdSportsCenterClient", // ajuste o nome da coluna se necessário
                principalTable: "SportsCenterClients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade); // ajuste a ação de delete conforme original
        }
    }
}