using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VideoMonitoramento.APIRest.Migrations
{
    public partial class tabelasiniciais : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Servidores",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    EnderecoIP = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PortaIP = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Servidores", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Videos",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    VideoContent = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    ServidorID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ServerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Videos", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Videos_Servidores_ServerId",
                        column: x => x.ServerId,
                        principalTable: "Servidores",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Videos_ServerId",
                table: "Videos",
                column: "ServerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Videos");

            migrationBuilder.DropTable(
                name: "Servidores");
        }
    }
}
