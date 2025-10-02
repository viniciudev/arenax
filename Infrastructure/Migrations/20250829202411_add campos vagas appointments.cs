using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addcamposvagasappointments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AvailablePositions");

            migrationBuilder.AddColumn<int>(
                name: "Number",
                table: "SportsCourtAppointments",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "OpenPublic",
                table: "SportsCourtAppointments",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "PaymentMethod",
                table: "SportsCourtAppointments",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "SportsCourtAppointments",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Value",
                table: "SportsCourtAppointments",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Number",
                table: "SportsCourtAppointments");

            migrationBuilder.DropColumn(
                name: "OpenPublic",
                table: "SportsCourtAppointments");

            migrationBuilder.DropColumn(
                name: "PaymentMethod",
                table: "SportsCourtAppointments");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "SportsCourtAppointments");

            migrationBuilder.DropColumn(
                name: "Value",
                table: "SportsCourtAppointments");

            migrationBuilder.CreateTable(
                name: "AvailablePositions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IdSportCourtCategory = table.Column<int>(type: "integer", nullable: false),
                    IdSportsCourtAppointments = table.Column<int>(type: "integer", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Number = table.Column<int>(type: "integer", nullable: false),
                    OpenPublic = table.Column<bool>(type: "boolean", nullable: false),
                    PaymentMethod = table.Column<string>(type: "text", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Value = table.Column<decimal>(type: "numeric", nullable: false)
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
    }
}
