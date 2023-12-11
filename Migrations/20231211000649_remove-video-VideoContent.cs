using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VideoMonitoramento.APIRest.Migrations
{
    public partial class removevideoVideoContent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VideoContent",
                table: "Videos");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "VideoContent",
                table: "Videos",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);
        }
    }
}
