using Microsoft.EntityFrameworkCore.Migrations;

namespace ViewMonitor.Migrations
{
    public partial class addhistproce : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProcesoId",
                table: "Monitor_Estado_Hists",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProcesoId",
                table: "Monitor_Estado_Hists");
        }
    }
}
