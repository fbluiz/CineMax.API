using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CineMax.Infra.Migrations
{
    /// <inheritdoc />
    public partial class RepairnameColumTicketsDisponible : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TickestDisponible",
                table: "Sections",
                newName: "TicketsDisponible");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TicketsDisponible",
                table: "Sections",
                newName: "TickestDisponible");
        }
    }
}
