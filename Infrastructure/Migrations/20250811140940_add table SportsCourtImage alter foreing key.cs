using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addtableSportsCourtImagealterforeingkey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SportsCourtImage_SportsCourt_Id",
                table: "SportsCourtImage");

            migrationBuilder.DropColumn(
                name: "UniqueFileName",
                table: "SportsCourt");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "SportsCourtImage",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.CreateIndex(
                name: "IX_SportsCourtImage_SportsCourtId",
                table: "SportsCourtImage",
                column: "SportsCourtId");

            migrationBuilder.AddForeignKey(
                name: "FK_SportsCourtImage_SportsCourt_SportsCourtId",
                table: "SportsCourtImage",
                column: "SportsCourtId",
                principalTable: "SportsCourt",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SportsCourtImage_SportsCourt_SportsCourtId",
                table: "SportsCourtImage");

            migrationBuilder.DropIndex(
                name: "IX_SportsCourtImage_SportsCourtId",
                table: "SportsCourtImage");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "SportsCourtImage",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<string>(
                name: "UniqueFileName",
                table: "SportsCourt",
                type: "text",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_SportsCourtImage_SportsCourt_Id",
                table: "SportsCourtImage",
                column: "Id",
                principalTable: "SportsCourt",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
