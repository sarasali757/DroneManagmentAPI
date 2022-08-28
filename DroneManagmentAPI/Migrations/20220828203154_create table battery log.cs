using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DroneManagmentAPI.Migrations
{
    public partial class createtablebatterylog : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BatteryLog",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Drone_ID = table.Column<int>(type: "int", nullable: false),
                    BatteryLevel = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    TimeLog = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BatteryLog", x => x.ID);
                    table.ForeignKey(
                        name: "FK_BatteryLog_Drones_Drone_ID",
                        column: x => x.Drone_ID,
                        principalTable: "Drones",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BatteryLog_Drone_ID",
                table: "BatteryLog",
                column: "Drone_ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BatteryLog");
        }
    }
}
