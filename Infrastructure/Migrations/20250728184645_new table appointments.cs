using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class newtableappointments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SportsCourtAppointments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    InitialDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    FinalDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    IdSportsCourt = table.Column<int>(type: "integer", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SportsCourtAppointments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SportsCourtAppointments_SportsCourt_IdSportsCourt",
                        column: x => x.IdSportsCourt,
                        principalTable: "SportsCourt",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SportsCourtAppointments_IdSportsCourt",
                table: "SportsCourtAppointments",
                column: "IdSportsCourt");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SportsCourtAppointments");
        }
    }
}
