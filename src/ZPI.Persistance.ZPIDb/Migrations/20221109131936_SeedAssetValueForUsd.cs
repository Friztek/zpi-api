using Microsoft.EntityFrameworkCore.Migrations;
using NodaTime;

#nullable disable

namespace ZPI.Persistance.ZPIDb.Migrations
{
    public partial class SeedAssetValueForUsd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                schema: "zpi",
                table: "AssetValues",
                columns: new[] { "Identifier", "AssetIdentifier", "TimeStamp", "Value" },
                values: new object[] { -1L, "usd", new NodaTime.OffsetDateTime(new NodaTime.LocalDateTime(1, 1, 1, 0, 0), NodaTime.Offset.FromHours(0)), 1.0 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "zpi",
                table: "AssetValues",
                keyColumn: "Identifier",
                keyValue: -1L);
        }
    }
}
