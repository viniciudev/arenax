using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addtableSportsCourtImage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SportsCourtImage",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    SportsCourtId = table.Column<int>(type: "integer", nullable: false),
                    UniqueFileName = table.Column<string>(type: "text", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SportsCourtImage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SportsCourtImage_SportsCourt_Id",
                        column: x => x.Id,
                        principalTable: "SportsCourt",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SportsCourtImage");
        }
    }
}
