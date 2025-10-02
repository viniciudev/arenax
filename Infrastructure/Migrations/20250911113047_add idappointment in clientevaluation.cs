using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addidappointmentinclientevaluation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IdAppointments",
                table: "ClientEvaluation",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ClientEvaluation_IdAppointments",
                table: "ClientEvaluation",
                column: "IdAppointments");

            migrationBuilder.AddForeignKey(
                name: "FK_ClientEvaluation_SportsCourtAppointments_IdAppointments",
                table: "ClientEvaluation",
                column: "IdAppointments",
                principalTable: "SportsCourtAppointments",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClientEvaluation_SportsCourtAppointments_IdAppointments",
                table: "ClientEvaluation");

            migrationBuilder.DropIndex(
                name: "IX_ClientEvaluation_IdAppointments",
                table: "ClientEvaluation");

            migrationBuilder.DropColumn(
                name: "IdAppointments",
                table: "ClientEvaluation");
        }
    }
}
