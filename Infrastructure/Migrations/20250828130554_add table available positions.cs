using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addtableavailablepositions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AvailablePositions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Number = table.Column<int>(type: "integer", nullable: false),
                    IdSportsCourtAppointments = table.Column<int>(type: "integer", nullable: false),
                    Value = table.Column<decimal>(type: "numeric", nullable: false),
                    OpenPublic = table.Column<bool>(type: "boolean", nullable: false),
                    IdSportCourtCategory = table.Column<int>(type: "integer", nullable: false),
                    PaymentMethod = table.Column<string>(type: "text", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AvailablePositions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AvailablePositions_SportsCourtAppointments_IdSportsCourtApp~",
                        column: x => x.IdSportsCourtAppointments,
                        principalTable: "SportsCourtAppointments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AvailablePositions_SportsCourtCategory_IdSportCourtCategory",
                        column: x => x.IdSportCourtCategory,
                        principalTable: "SportsCourtCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AvailablePositions_IdSportCourtCategory",
                table: "AvailablePositions",
                column: "IdSportCourtCategory");

            migrationBuilder.CreateIndex(
                name: "IX_AvailablePositions_IdSportsCourtAppointments",
                table: "AvailablePositions",
                column: "IdSportsCourtAppointments");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AvailablePositions");
        }
    }
}
