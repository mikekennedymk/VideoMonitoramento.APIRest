using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VideoMonitoramento.APIRest.Migrations
{
    public partial class FKservidor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Videos_Servidores_ServerId",
                table: "Videos");

            migrationBuilder.DropIndex(
                name: "IX_Videos_ServerId",
                table: "Videos");

            migrationBuilder.DropColumn(
                name: "ServerId",
                table: "Videos");

            migrationBuilder.CreateIndex(
                name: "IX_Videos_ServidorID",
                table: "Videos",
                column: "ServidorID");

            migrationBuilder.AddForeignKey(
                name: "FK_Videos_Servidores_ServidorID",
                table: "Videos",
                column: "ServidorID",
                principalTable: "Servidores",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Videos_Servidores_ServidorID",
                table: "Videos");

            migrationBuilder.DropIndex(
                name: "IX_Videos_ServidorID",
                table: "Videos");

            migrationBuilder.AddColumn<Guid>(
                name: "ServerId",
                table: "Videos",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Videos_ServerId",
                table: "Videos",
                column: "ServerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Videos_Servidores_ServerId",
                table: "Videos",
                column: "ServerId",
                principalTable: "Servidores",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
