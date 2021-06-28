using Microsoft.EntityFrameworkCore.Migrations;

namespace ViewMonitor.Migrations
{
    public partial class KeyMonitor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "KeyMonitorProce",
                table: "Monitors",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "KeyMonitorProce",
                table: "Monitors");
        }
    }
}
