using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ZPI.Persistance.ZPIDb.Migrations
{
    public partial class AddPrimaryKeyForAssetValue : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_AssetValues",
                schema: "zpi",
                table: "AssetValues");

            migrationBuilder.AddColumn<long>(
                name: "Identifier",
                schema: "zpi",
                table: "AssetValues",
                type: "bigint",
                nullable: false,
                defaultValue: 0L)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_AssetValues",
                schema: "zpi",
                table: "AssetValues",
                column: "Identifier");

            migrationBuilder.CreateIndex(
                name: "IX_AssetValues_AssetIdentifier",
                schema: "zpi",
                table: "AssetValues",
                column: "AssetIdentifier");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_AssetValues",
                schema: "zpi",
                table: "AssetValues");

            migrationBuilder.DropIndex(
                name: "IX_AssetValues_AssetIdentifier",
                schema: "zpi",
                table: "AssetValues");

            migrationBuilder.DropColumn(
                name: "Identifier",
                schema: "zpi",
                table: "AssetValues");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AssetValues",
                schema: "zpi",
                table: "AssetValues",
                column: "AssetIdentifier");
        }
    }
}
