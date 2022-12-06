using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZPI.Persistance.ZPIDb.Migrations
{
    public partial class AddMissingRelationsForUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Alerts",
                schema: "zpi");

            migrationBuilder.CreateIndex(
                name: "IX_Wallets_UserIdentifier",
                schema: "zpi",
                table: "Wallets",
                column: "UserIdentifier");

            migrationBuilder.CreateIndex(
                name: "IX_UserAssets_UserId",
                schema: "zpi",
                table: "UserAssets",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_UserIdentifier",
                schema: "zpi",
                table: "Transactions",
                column: "UserIdentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_UserPreferences_UserIdentifier",
                schema: "zpi",
                table: "Transactions",
                column: "UserIdentifier",
                principalSchema: "zpi",
                principalTable: "UserPreferences",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserAssets_UserPreferences_UserId",
                schema: "zpi",
                table: "UserAssets",
                column: "UserId",
                principalSchema: "zpi",
                principalTable: "UserPreferences",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Wallets_UserPreferences_UserIdentifier",
                schema: "zpi",
                table: "Wallets",
                column: "UserIdentifier",
                principalSchema: "zpi",
                principalTable: "UserPreferences",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_UserPreferences_UserIdentifier",
                schema: "zpi",
                table: "Transactions");

            migrationBuilder.DropForeignKey(
                name: "FK_UserAssets_UserPreferences_UserId",
                schema: "zpi",
                table: "UserAssets");

            migrationBuilder.DropForeignKey(
                name: "FK_Wallets_UserPreferences_UserIdentifier",
                schema: "zpi",
                table: "Wallets");

            migrationBuilder.DropIndex(
                name: "IX_Wallets_UserIdentifier",
                schema: "zpi",
                table: "Wallets");

            migrationBuilder.DropIndex(
                name: "IX_UserAssets_UserId",
                schema: "zpi",
                table: "UserAssets");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_UserIdentifier",
                schema: "zpi",
                table: "Transactions");

            migrationBuilder.CreateTable(
                name: "Alerts",
                schema: "zpi",
                columns: table => new
                {
                    Identifier = table.Column<int>(type: "integer", nullable: false),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    OriginAssetId = table.Column<string>(type: "text", nullable: false),
                    TargetCurrency = table.Column<string>(type: "text", nullable: false),
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

            migrationBuilder.CreateIndex(
                name: "IX_Alerts_OriginAssetId",
                schema: "zpi",
                table: "Alerts",
                column: "OriginAssetId");
        }
    }
}
