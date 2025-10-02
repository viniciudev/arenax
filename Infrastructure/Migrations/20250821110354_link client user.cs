using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class linkclientuser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IdClient",
                table: "User",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_User_IdClient",
                table: "User",
                column: "IdClient",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_User_Client_IdClient",
                table: "User",
                column: "IdClient",
                principalTable: "Client",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_Client_IdClient",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_User_IdClient",
                table: "User");

            migrationBuilder.DropColumn(
                name: "IdClient",
                table: "User");
        }
    }
}
