using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addtableSportsRegistration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SportsRegistration",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IdSportsCourtAppointments = table.Column<int>(type: "integer", nullable: false),
                    IdClient = table.Column<int>(type: "integer", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SportsRegistration", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SportsRegistration_Client_IdClient",
                        column: x => x.IdClient,
                        principalTable: "Client",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SportsRegistration_SportsCourtAppointments_IdSportsCourtApp~",
                        column: x => x.IdSportsCourtAppointments,
                        principalTable: "SportsCourtAppointments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SportsRegistration_IdClient",
                table: "SportsRegistration",
                column: "IdClient");

            migrationBuilder.CreateIndex(
                name: "IX_SportsRegistration_IdSportsCourtAppointments",
                table: "SportsRegistration",
                column: "IdSportsCourtAppointments");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SportsRegistration");
        }
    }
}
