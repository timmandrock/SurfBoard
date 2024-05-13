using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SurfAndTurf.Migrations
{
    public partial class Add_Boking : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Bookings",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdentityUserID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    SurfBoardID = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bookings", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Bookings_AspNetUsers_IdentityUserID",
                        column: x => x.IdentityUserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Bookings_SurfBoard_SurfBoardID",
                        column: x => x.SurfBoardID,
                        principalTable: "SurfBoard",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_IdentityUserID",
                table: "Bookings",
                column: "IdentityUserID");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_SurfBoardID",
                table: "Bookings",
                column: "SurfBoardID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bookings");
        }
    }
}
