using Microsoft.EntityFrameworkCore.Migrations;
using NodaTime;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ZPI.Persistance.ZPIDb.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "zpi");

            migrationBuilder.CreateTable(
                name: "Assets",
                schema: "zpi",
                columns: table => new
                {
                    Identifier = table.Column<string>(type: "text", nullable: false),
                    FriendlyName = table.Column<string>(type: "text", nullable: false),
                    Category = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Assets", x => x.Identifier);
                });

            migrationBuilder.CreateTable(
                name: "Alerts",
                schema: "zpi",
                columns: table => new
                {
                    Identifier = table.Column<int>(type: "integer", nullable: false),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    TargetCurrency = table.Column<string>(type: "text", nullable: false),
                    OriginAssetId = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Alerts", x => new { x.Identifier, x.UserId });
                    table.ForeignKey(
                        name: "FK_Alerts_Assets_OriginAssetId",
                        column: x => x.OriginAssetId,
                        principalSchema: "zpi",
                        principalTable: "Assets",
                        principalColumn: "Identifier",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AssetValues",
                schema: "zpi",
                columns: table => new
                {
                    AssetIdentifier = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<double>(type: "double precision", nullable: false),
                    TimeStamp = table.Column<OffsetDateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssetValues", x => x.AssetIdentifier);
                    table.ForeignKey(
                        name: "FK_AssetValues_Assets_AssetIdentifier",
                        column: x => x.AssetIdentifier,
                        principalSchema: "zpi",
                        principalTable: "Assets",
                        principalColumn: "Identifier",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Transactions",
                schema: "zpi",
                columns: table => new
                {
                    Identifier = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserIdentifier = table.Column<string>(type: "text", nullable: false),
                    AssetIdentifier = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<double>(type: "double precision", nullable: false),
                    TimeStamp = table.Column<OffsetDateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.Identifier);
                    table.ForeignKey(
                        name: "FK_Transactions_Assets_AssetIdentifier",
                        column: x => x.AssetIdentifier,
                        principalSchema: "zpi",
                        principalTable: "Assets",
                        principalColumn: "Identifier",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserAssets",
                schema: "zpi",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "text", nullable: false),
                    AssetIdentifier = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAssets", x => new { x.AssetIdentifier, x.UserId });
                    table.ForeignKey(
                        name: "FK_UserAssets_Assets_AssetIdentifier",
                        column: x => x.AssetIdentifier,
                        principalSchema: "zpi",
                        principalTable: "Assets",
                        principalColumn: "Identifier",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Alerts_OriginAssetId",
                schema: "zpi",
                table: "Alerts",
                column: "OriginAssetId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_AssetIdentifier",
                schema: "zpi",
                table: "Transactions",
                column: "AssetIdentifier");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Alerts",
                schema: "zpi");

            migrationBuilder.DropTable(
                name: "AssetValues",
                schema: "zpi");

            migrationBuilder.DropTable(
                name: "Transactions",
                schema: "zpi");

            migrationBuilder.DropTable(
                name: "UserAssets",
                schema: "zpi");

            migrationBuilder.DropTable(
                name: "Assets",
                schema: "zpi");
        }
    }
}
