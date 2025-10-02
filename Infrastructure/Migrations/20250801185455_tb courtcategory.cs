using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class tbcourtcategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SportsCategory_SportsCourt_IdSportsCourt",
                table: "SportsCategory");

            migrationBuilder.DropIndex(
                name: "IX_SportsCategory_IdSportsCourt",
                table: "SportsCategory");

            migrationBuilder.DropColumn(
                name: "IdSportsCourt",
                table: "SportsCategory");

            migrationBuilder.CreateTable(
                name: "SportsCourtCategory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SportsCourtId = table.Column<int>(type: "integer", nullable: false),
                    SportsCategoryId = table.Column<int>(type: "integer", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SportsCourtCategory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SportsCourtCategory_SportsCategory_SportsCategoryId",
                        column: x => x.SportsCategoryId,
                        principalTable: "SportsCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SportsCourtCategory_SportsCourt_SportsCourtId",
                        column: x => x.SportsCourtId,
                        principalTable: "SportsCourt",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SportsCourtCategory_SportsCategoryId",
                table: "SportsCourtCategory",
                column: "SportsCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_SportsCourtCategory_SportsCourtId",
                table: "SportsCourtCategory",
                column: "SportsCourtId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SportsCourtCategory");

            migrationBuilder.AddColumn<int>(
                name: "IdSportsCourt",
                table: "SportsCategory",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_SportsCategory_IdSportsCourt",
                table: "SportsCategory",
                column: "IdSportsCourt");

            migrationBuilder.AddForeignKey(
                name: "FK_SportsCategory_SportsCourt_IdSportsCourt",
                table: "SportsCategory",
                column: "IdSportsCourt",
                principalTable: "SportsCourt",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
