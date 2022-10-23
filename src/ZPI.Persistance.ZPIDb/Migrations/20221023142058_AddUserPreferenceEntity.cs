using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZPI.Persistance.ZPIDb.Migrations
{
    public partial class AddUserPreferenceEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserPreferences",
                schema: "zpi",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "text", nullable: false),
                    PreferenceCurrency = table.Column<string>(type: "text", nullable: false),
                    WeeklyReports = table.Column<bool>(type: "boolean", nullable: false),
                    AlertsOnEmail = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserPreferences", x => x.UserId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserPreferences",
                schema: "zpi");
        }
    }
}
