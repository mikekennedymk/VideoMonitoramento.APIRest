using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VideoMonitoramento.APIRest.Migrations
{
    public partial class addcaminhovideo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Caminho",
                table: "Videos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Caminho",
                table: "Videos");
        }
    }
}
