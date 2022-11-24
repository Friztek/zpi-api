using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZPI.Persistance.ZPIDb.Migrations
{
    public partial class AddDescriptionToUserAssetPrimaryKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_UserAssets",
                schema: "zpi",
                table: "UserAssets");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserAssets",
                schema: "zpi",
                table: "UserAssets",
                columns: new[] { "AssetIdentifier", "UserId", "Description" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_UserAssets",
                schema: "zpi",
                table: "UserAssets");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserAssets",
                schema: "zpi",
                table: "UserAssets",
                columns: new[] { "AssetIdentifier", "UserId" });
        }
    }
}
