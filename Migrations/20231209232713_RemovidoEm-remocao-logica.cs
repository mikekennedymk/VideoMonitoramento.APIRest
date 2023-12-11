using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VideoMonitoramento.APIRest.Migrations
{
    public partial class RemovidoEmremocaologica : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "RemovidoEm",
                table: "Videos",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "EnderecoIP",
                table: "Servidores",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<DateTime>(
                name: "RemovidoEm",
                table: "Servidores",
                type: "datetime2",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RemovidoEm",
                table: "Videos");

            migrationBuilder.DropColumn(
                name: "RemovidoEm",
                table: "Servidores");

            migrationBuilder.AlterColumn<string>(
                name: "EnderecoIP",
                table: "Servidores",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(15)",
                oldMaxLength: 15);
        }
    }
}
