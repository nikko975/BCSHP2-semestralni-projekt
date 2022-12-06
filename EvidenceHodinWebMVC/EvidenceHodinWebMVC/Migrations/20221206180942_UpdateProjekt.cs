using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EvidenceHodinWebMVC.Migrations
{
    /// <inheritdoc />
    public partial class UpdateProjekt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProjektId",
                table: "Cinnost",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cinnost_ProjektId",
                table: "Cinnost",
                column: "ProjektId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cinnost_Projekt_ProjektId",
                table: "Cinnost",
                column: "ProjektId",
                principalTable: "Projekt",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cinnost_Projekt_ProjektId",
                table: "Cinnost");

            migrationBuilder.DropIndex(
                name: "IX_Cinnost_ProjektId",
                table: "Cinnost");

            migrationBuilder.DropColumn(
                name: "ProjektId",
                table: "Cinnost");
        }
    }
}
