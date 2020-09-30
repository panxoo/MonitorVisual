using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ViewMonitor.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Agrupacions",
                columns: table => new
                {
                    AgrupacionID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Agrupacions", x => x.AgrupacionID);
                });

            migrationBuilder.CreateTable(
                name: "Job_Monitors",
                columns: table => new
                {
                    Job_MonitorID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Job_Monitors", x => x.Job_MonitorID);
                });

            migrationBuilder.CreateTable(
                name: "Monitors",
                columns: table => new
                {
                    MonitorID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(maxLength: 500, nullable: true),
                    Procedimiento = table.Column<string>(maxLength: 1000, nullable: true),
                    Activo = table.Column<bool>(nullable: false),
                    Job_MonitorID = table.Column<int>(nullable: false),
                    AgrupacionID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Monitors", x => x.MonitorID);
                    table.ForeignKey(
                        name: "FK_Monitors_Agrupacions_AgrupacionID",
                        column: x => x.AgrupacionID,
                        principalTable: "Agrupacions",
                        principalColumn: "AgrupacionID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Monitors_Job_Monitors_Job_MonitorID",
                        column: x => x.Job_MonitorID,
                        principalTable: "Job_Monitors",
                        principalColumn: "Job_MonitorID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Monitor_Estado_Hists",
                columns: table => new
                {
                    Monitor_Estado_HistID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    estado = table.Column<bool>(nullable: false),
                    Fecha = table.Column<DateTime>(nullable: false),
                    MonitorID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Monitor_Estado_Hists", x => x.Monitor_Estado_HistID);
                    table.ForeignKey(
                        name: "FK_Monitor_Estado_Hists_Monitors_MonitorID",
                        column: x => x.MonitorID,
                        principalTable: "Monitors",
                        principalColumn: "MonitorID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Monitor_Estados",
                columns: table => new
                {
                    Monitor_EstadoID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Estado = table.Column<bool>(nullable: false),
                    Fecha = table.Column<DateTime>(nullable: false),
                    MonitorID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Monitor_Estados", x => x.Monitor_EstadoID);
                    table.ForeignKey(
                        name: "FK_Monitor_Estados_Monitors_MonitorID",
                        column: x => x.MonitorID,
                        principalTable: "Monitors",
                        principalColumn: "MonitorID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Monitor_Estado_Hists_MonitorID",
                table: "Monitor_Estado_Hists",
                column: "MonitorID");

            migrationBuilder.CreateIndex(
                name: "IX_Monitor_Estados_MonitorID",
                table: "Monitor_Estados",
                column: "MonitorID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Monitors_AgrupacionID",
                table: "Monitors",
                column: "AgrupacionID");

            migrationBuilder.CreateIndex(
                name: "IX_Monitors_Job_MonitorID",
                table: "Monitors",
                column: "Job_MonitorID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Monitor_Estado_Hists");

            migrationBuilder.DropTable(
                name: "Monitor_Estados");

            migrationBuilder.DropTable(
                name: "Monitors");

            migrationBuilder.DropTable(
                name: "Agrupacions");

            migrationBuilder.DropTable(
                name: "Job_Monitors");
        }
    }
}
