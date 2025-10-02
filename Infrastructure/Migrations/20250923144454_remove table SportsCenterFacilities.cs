using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class removetableSportsCenterFacilities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SportsCenterFacilities");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SportsCenterFacilities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IdSportsCenter = table.Column<int>(type: "integer", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    SvgContent = table.Column<string>(type: "text", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SportsCenterFacilities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SportsCenterFacilities_SportsCenter_IdSportsCenter",
                        column: x => x.IdSportsCenter,
                        principalTable: "SportsCenter",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SportsCenterFacilities_IdSportsCenter",
                table: "SportsCenterFacilities",
                column: "IdSportsCenter");
        }
    }
}
