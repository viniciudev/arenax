using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ajusteforeignkeysportscourt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourtEvaluations_SportsCourt_SportsCourtId",
                table: "CourtEvaluations");

            migrationBuilder.DropForeignKey(
                name: "FK_SportsCategory_SportsCourt_SportsCourtId",
                table: "SportsCategory");

            migrationBuilder.DropForeignKey(
                name: "FK_SportsCourtOperation_SportsCourt_SportsCourtId",
                table: "SportsCourtOperation");

            migrationBuilder.DropIndex(
                name: "IX_SportsCourtOperation_SportsCourtId",
                table: "SportsCourtOperation");

            migrationBuilder.DropIndex(
                name: "IX_SportsCategory_SportsCourtId",
                table: "SportsCategory");

            migrationBuilder.DropIndex(
                name: "IX_CourtEvaluations_SportsCourtId",
                table: "CourtEvaluations");

            migrationBuilder.DropColumn(
                name: "SportsCourtId",
                table: "SportsCourtOperation");

            migrationBuilder.DropColumn(
                name: "SportsCourtId",
                table: "SportsCategory");

            migrationBuilder.DropColumn(
                name: "SportsCourtId",
                table: "CourtEvaluations");

            migrationBuilder.CreateIndex(
                name: "IX_SportsCourtOperation_IdSportsCourt",
                table: "SportsCourtOperation",
                column: "IdSportsCourt");

            migrationBuilder.CreateIndex(
                name: "IX_SportsCategory_IdSportsCourt",
                table: "SportsCategory",
                column: "IdSportsCourt");

            migrationBuilder.CreateIndex(
                name: "IX_CourtEvaluations_IdSportsCourt",
                table: "CourtEvaluations",
                column: "IdSportsCourt");

            migrationBuilder.AddForeignKey(
                name: "FK_CourtEvaluations_SportsCourt_IdSportsCourt",
                table: "CourtEvaluations",
                column: "IdSportsCourt",
                principalTable: "SportsCourt",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SportsCategory_SportsCourt_IdSportsCourt",
                table: "SportsCategory",
                column: "IdSportsCourt",
                principalTable: "SportsCourt",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SportsCourtOperation_SportsCourt_IdSportsCourt",
                table: "SportsCourtOperation",
                column: "IdSportsCourt",
                principalTable: "SportsCourt",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourtEvaluations_SportsCourt_IdSportsCourt",
                table: "CourtEvaluations");

            migrationBuilder.DropForeignKey(
                name: "FK_SportsCategory_SportsCourt_IdSportsCourt",
                table: "SportsCategory");

            migrationBuilder.DropForeignKey(
                name: "FK_SportsCourtOperation_SportsCourt_IdSportsCourt",
                table: "SportsCourtOperation");

            migrationBuilder.DropIndex(
                name: "IX_SportsCourtOperation_IdSportsCourt",
                table: "SportsCourtOperation");

            migrationBuilder.DropIndex(
                name: "IX_SportsCategory_IdSportsCourt",
                table: "SportsCategory");

            migrationBuilder.DropIndex(
                name: "IX_CourtEvaluations_IdSportsCourt",
                table: "CourtEvaluations");

            migrationBuilder.AddColumn<int>(
                name: "SportsCourtId",
                table: "SportsCourtOperation",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SportsCourtId",
                table: "SportsCategory",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SportsCourtId",
                table: "CourtEvaluations",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_SportsCourtOperation_SportsCourtId",
                table: "SportsCourtOperation",
                column: "SportsCourtId");

            migrationBuilder.CreateIndex(
                name: "IX_SportsCategory_SportsCourtId",
                table: "SportsCategory",
                column: "SportsCourtId");

            migrationBuilder.CreateIndex(
                name: "IX_CourtEvaluations_SportsCourtId",
                table: "CourtEvaluations",
                column: "SportsCourtId");

            migrationBuilder.AddForeignKey(
                name: "FK_CourtEvaluations_SportsCourt_SportsCourtId",
                table: "CourtEvaluations",
                column: "SportsCourtId",
                principalTable: "SportsCourt",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SportsCategory_SportsCourt_SportsCourtId",
                table: "SportsCategory",
                column: "SportsCourtId",
                principalTable: "SportsCourt",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SportsCourtOperation_SportsCourt_SportsCourtId",
                table: "SportsCourtOperation",
                column: "SportsCourtId",
                principalTable: "SportsCourt",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
