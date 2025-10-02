using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addcollunidcategoryinSportsCourtAppointments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IdCategory",
                table: "SportsCourtAppointments",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SportsCourtAppointments_IdCategory",
                table: "SportsCourtAppointments",
                column: "IdCategory");

            migrationBuilder.AddForeignKey(
                name: "FK_SportsCourtAppointments_SportsCategory_IdCategory",
                table: "SportsCourtAppointments",
                column: "IdCategory",
                principalTable: "SportsCategory",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SportsCourtAppointments_SportsCategory_IdCategory",
                table: "SportsCourtAppointments");

            migrationBuilder.DropIndex(
                name: "IX_SportsCourtAppointments_IdCategory",
                table: "SportsCourtAppointments");

            migrationBuilder.DropColumn(
                name: "IdCategory",
                table: "SportsCourtAppointments");
        }
    }
}
