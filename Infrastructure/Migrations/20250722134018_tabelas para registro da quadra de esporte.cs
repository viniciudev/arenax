using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class tabelaspararegistrodaquadradeesporte : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SportsCourt",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    SubName = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Price = table.Column<decimal>(type: "numeric", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SportsCourt", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CourtEvaluations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Note = table.Column<int>(type: "integer", nullable: false),
                    SportsCourtId = table.Column<int>(type: "integer", nullable: false),
                    IdSportsCourt = table.Column<int>(type: "integer", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourtEvaluations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CourtEvaluations_SportsCourt_SportsCourtId",
                        column: x => x.SportsCourtId,
                        principalTable: "SportsCourt",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SportsCategory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Description = table.Column<string>(type: "text", nullable: false),
                    SportsCourtId = table.Column<int>(type: "integer", nullable: false),
                    IdSportsCourt = table.Column<int>(type: "integer", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SportsCategory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SportsCategory_SportsCourt_SportsCourtId",
                        column: x => x.SportsCourtId,
                        principalTable: "SportsCourt",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SportsCourtOperation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DayOfWeek = table.Column<int>(type: "integer", nullable: false),
                    Start = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    End = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    SportsCourtId = table.Column<int>(type: "integer", nullable: false),
                    IdSportsCourt = table.Column<int>(type: "integer", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SportsCourtOperation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SportsCourtOperation_SportsCourt_SportsCourtId",
                        column: x => x.SportsCourtId,
                        principalTable: "SportsCourt",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CourtEvaluations_SportsCourtId",
                table: "CourtEvaluations",
                column: "SportsCourtId");

            migrationBuilder.CreateIndex(
                name: "IX_SportsCategory_SportsCourtId",
                table: "SportsCategory",
                column: "SportsCourtId");

            migrationBuilder.CreateIndex(
                name: "IX_SportsCourtOperation_SportsCourtId",
                table: "SportsCourtOperation",
                column: "SportsCourtId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CourtEvaluations");

            migrationBuilder.DropTable(
                name: "SportsCategory");

            migrationBuilder.DropTable(
                name: "SportsCourtOperation");

            migrationBuilder.DropTable(
                name: "SportsCourt");
        }
    }
}
