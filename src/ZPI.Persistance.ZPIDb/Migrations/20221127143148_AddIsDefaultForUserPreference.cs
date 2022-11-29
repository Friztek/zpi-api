using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZPI.Persistance.ZPIDb.Migrations
{
    public partial class AddIsDefaultForUserPreference : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDefault",
                schema: "zpi",
                table: "UserPreferences",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDefault",
                schema: "zpi",
                table: "UserPreferences");
        }
    }
}
