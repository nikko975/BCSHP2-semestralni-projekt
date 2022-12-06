using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EvidenceHodinWebMVC.Migrations
{
    /// <inheritdoc />
    public partial class UpdateProjectAktivity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Aktivita",
                table: "Zakaznik",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Aktivita",
                table: "Projekt",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Aktivita",
                table: "Zakaznik");

            migrationBuilder.DropColumn(
                name: "Aktivita",
                table: "Projekt");
        }
    }
}
