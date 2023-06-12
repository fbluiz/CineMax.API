using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CineMax.Infra.Migrations
{
    /// <inheritdoc />
    public partial class Alter : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MaximumTickets",
                table: "Sections",
                newName: "TickestDisponible");

            migrationBuilder.AddColumn<int>(
                name: "MaximumTicket",
                table: "Rooms",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MaximumTicket",
                table: "Rooms");

            migrationBuilder.RenameColumn(
                name: "TickestDisponible",
                table: "Sections",
                newName: "MaximumTickets");
        }
    }
}
