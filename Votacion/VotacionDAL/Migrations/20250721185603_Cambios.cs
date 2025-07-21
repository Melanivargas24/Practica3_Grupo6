using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VotacionDAL.Migrations
{
    /// <inheritdoc />
    public partial class Cambios : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Votos_Votantes_CedulaVotante",
                table: "Votos");

            migrationBuilder.DropIndex(
                name: "IX_Votos_CedulaVotante",
                table: "Votos");

            migrationBuilder.CreateIndex(
                name: "IX_Votos_CedulaVotante",
                table: "Votos",
                column: "CedulaVotante");

            migrationBuilder.AddForeignKey(
                name: "FK_Votos_Votantes_CedulaVotante",
                table: "Votos",
                column: "CedulaVotante",
                principalTable: "Votantes",
                principalColumn: "Cedula",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Votos_Votantes_CedulaVotante",
                table: "Votos");

            migrationBuilder.DropIndex(
                name: "IX_Votos_CedulaVotante",
                table: "Votos");

            migrationBuilder.CreateIndex(
                name: "IX_Votos_CedulaVotante",
                table: "Votos",
                column: "CedulaVotante",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Votos_Votantes_CedulaVotante",
                table: "Votos",
                column: "CedulaVotante",
                principalTable: "Votantes",
                principalColumn: "Cedula",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
