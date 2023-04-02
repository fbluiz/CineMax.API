using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CineMax.Infra.Migrations
{
    /// <inheritdoc />
    public partial class addremovedall : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Removed",
                table: "Users",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RemovedOn",
                table: "Users",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Removed",
                table: "Tickets",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RemovedOn",
                table: "Tickets",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Removed",
                table: "Sections",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RemovedOn",
                table: "Sections",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Removed",
                table: "Seats",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RemovedOn",
                table: "Seats",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Removed",
                table: "Rooms",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RemovedOn",
                table: "Rooms",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Removed",
                table: "Movies",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RemovedOn",
                table: "Movies",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Removed",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "RemovedOn",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Removed",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "RemovedOn",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "Removed",
                table: "Sections");

            migrationBuilder.DropColumn(
                name: "RemovedOn",
                table: "Sections");

            migrationBuilder.DropColumn(
                name: "Removed",
                table: "Seats");

            migrationBuilder.DropColumn(
                name: "RemovedOn",
                table: "Seats");

            migrationBuilder.DropColumn(
                name: "Removed",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "RemovedOn",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "Removed",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "RemovedOn",
                table: "Movies");
        }
    }
}
