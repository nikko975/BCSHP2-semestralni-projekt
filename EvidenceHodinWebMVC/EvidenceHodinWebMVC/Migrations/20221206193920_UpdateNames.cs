using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EvidenceHodinWebMVC.Migrations
{
    /// <inheritdoc />
    public partial class UpdateNames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Zakaznik",
                newName: "ZakaznikId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Projekt",
                newName: "ProjektId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Contact",
                newName: "ContactId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Cinnost",
                newName: "CinnostId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ZakaznikId",
                table: "Zakaznik",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "ProjektId",
                table: "Projekt",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "ContactId",
                table: "Contact",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "CinnostId",
                table: "Cinnost",
                newName: "Id");
        }
    }
}
