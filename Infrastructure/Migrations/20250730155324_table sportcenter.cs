using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class tablesportcenter : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SportsCenter",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Cnpj = table.Column<string>(type: "text", nullable: true),
                    Phone = table.Column<string>(type: "text", nullable: true),
                    Logo = table.Column<byte[]>(type: "bytea", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SportsCenter", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Address",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Cep = table.Column<string>(type: "text", nullable: true),
                    StreetAddress = table.Column<string>(type: "text", nullable: true),
                    Complement = table.Column<string>(type: "text", nullable: true),
                    District = table.Column<string>(type: "text", nullable: true),
                    City = table.Column<string>(type: "text", nullable: true),
                    Uf = table.Column<string>(type: "text", nullable: true),
                    State = table.Column<string>(type: "text", nullable: true),
                    SportsCenterId = table.Column<int>(type: "integer", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Address", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Address_SportsCenter_SportsCenterId",
                        column: x => x.SportsCenterId,
                        principalTable: "SportsCenter",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OpeningHours",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SportsCenterId = table.Column<int>(type: "integer", nullable: false),
                    Monday = table.Column<string>(type: "text", nullable: true),
                    Tuesday = table.Column<string>(type: "text", nullable: true),
                    Wednesday = table.Column<string>(type: "text", nullable: true),
                    Thursday = table.Column<string>(type: "text", nullable: true),
                    Friday = table.Column<string>(type: "text", nullable: true),
                    Saturday = table.Column<string>(type: "text", nullable: true),
                    Sunday = table.Column<string>(type: "text", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OpeningHours", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OpeningHours_SportsCenter_SportsCenterId",
                        column: x => x.SportsCenterId,
                        principalTable: "SportsCenter",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SportsCenterUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IdUser = table.Column<int>(type: "integer", nullable: false),
                    IdSportsCenter = table.Column<int>(type: "integer", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SportsCenterUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SportsCenterUsers_SportsCenter_IdSportsCenter",
                        column: x => x.IdSportsCenter,
                        principalTable: "SportsCenter",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SportsCenterUsers_User_IdUser",
                        column: x => x.IdUser,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Address_SportsCenterId",
                table: "Address",
                column: "SportsCenterId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_OpeningHours_SportsCenterId",
                table: "OpeningHours",
                column: "SportsCenterId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SportsCenterUsers_IdSportsCenter",
                table: "SportsCenterUsers",
                column: "IdSportsCenter");

            migrationBuilder.CreateIndex(
                name: "IX_SportsCenterUsers_IdUser",
                table: "SportsCenterUsers",
                column: "IdUser");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Address");

            migrationBuilder.DropTable(
                name: "OpeningHours");

            migrationBuilder.DropTable(
                name: "SportsCenterUsers");

            migrationBuilder.DropTable(
                name: "SportsCenter");
        }
    }
}
