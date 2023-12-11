using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VideoMonitoramento.APIRest.Migrations
{
    public partial class addcriadoem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CriadoEm",
                table: "Videos",
                type: "datetime2",
                nullable: false,
                defaultValue: DateTime.Now);

            migrationBuilder.AddColumn<DateTime>(
                name: "CriadoEm",
                table: "Servidores",
                type: "datetime2",
                nullable: false,
                defaultValue: DateTime.Now);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CriadoEm",
                table: "Videos");

            migrationBuilder.DropColumn(
                name: "CriadoEm",
                table: "Servidores");
        }
    }
}
