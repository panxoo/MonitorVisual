using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ViewMonitor.Migrations
{
    public partial class UltimoEstado2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FechaEstado",
                table: "Monitor_Estados");

            migrationBuilder.DropColumn(
                name: "PeriodoError",
                table: "Monitor_Estados");

            migrationBuilder.CreateTable(
                name: "Monitor_Estado_Ultimos",
                columns: table => new
                {
                    Monitor_Estado_UltimoID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PeriodoError = table.Column<string>(nullable: true),
                    FechaEstado = table.Column<DateTime>(nullable: false),
                    MonitorID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Monitor_Estado_Ultimos", x => x.Monitor_Estado_UltimoID);
                    table.ForeignKey(
                        name: "FK_Monitor_Estado_Ultimos_Monitors_MonitorID",
                        column: x => x.MonitorID,
                        principalTable: "Monitors",
                        principalColumn: "MonitorID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Monitor_Estado_Ultimos_MonitorID",
                table: "Monitor_Estado_Ultimos",
                column: "MonitorID",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Monitor_Estado_Ultimos");

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaEstado",
                table: "Monitor_Estados",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "PeriodoError",
                table: "Monitor_Estados",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
