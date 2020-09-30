using Microsoft.EntityFrameworkCore.Migrations;

namespace ViewMonitor.Migrations
{
    public partial class ssa : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Monitors");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Monitors",
                columns: table => new
                {
                    id_monitor = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    n_monitor = table.Column<short>(type: "smallint", nullable: false),
                    nom_monitor = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    procedimiento = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Monitors", x => x.id_monitor);
                });
        }
    }
}
