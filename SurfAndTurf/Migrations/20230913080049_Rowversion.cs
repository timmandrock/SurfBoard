using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SurfAndTurf.Migrations
{
    public partial class Rowversion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "Rowversion",
                table: "SurfBoard",
                type: "rowversion",
                rowVersion: true,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Rowversion",
                table: "SurfBoard");
        }
    }
}
