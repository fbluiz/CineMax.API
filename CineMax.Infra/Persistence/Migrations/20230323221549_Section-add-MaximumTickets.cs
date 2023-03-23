using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CineMax.Infra.Migrations
{
    /// <inheritdoc />
    public partial class SectionaddMaximumTickets : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MaximumTickets",
                table: "Sections",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MaximumTickets",
                table: "Sections");
        }
    }
}
