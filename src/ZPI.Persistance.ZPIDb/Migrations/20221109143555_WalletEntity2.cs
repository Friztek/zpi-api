using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZPI.Persistance.ZPIDb.Migrations
{
    public partial class WalletEntity2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TransactionEntity_Assets_AssetIdentifier",
                schema: "zpi",
                table: "TransactionEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WalletEntity",
                schema: "zpi",
                table: "WalletEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TransactionEntity",
                schema: "zpi",
                table: "TransactionEntity");

            migrationBuilder.RenameTable(
                name: "WalletEntity",
                schema: "zpi",
                newName: "Wallets",
                newSchema: "zpi");

            migrationBuilder.RenameTable(
                name: "TransactionEntity",
                schema: "zpi",
                newName: "Transactions",
                newSchema: "zpi");

            migrationBuilder.RenameIndex(
                name: "IX_TransactionEntity_AssetIdentifier",
                schema: "zpi",
                table: "Transactions",
                newName: "IX_Transactions_AssetIdentifier");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Wallets",
                schema: "zpi",
                table: "Wallets",
                column: "Identifier");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Transactions",
                schema: "zpi",
                table: "Transactions",
                column: "Identifier");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Assets_AssetIdentifier",
                schema: "zpi",
                table: "Transactions",
                column: "AssetIdentifier",
                principalSchema: "zpi",
                principalTable: "Assets",
                principalColumn: "Identifier",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Assets_AssetIdentifier",
                schema: "zpi",
                table: "Transactions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Wallets",
                schema: "zpi",
                table: "Wallets");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Transactions",
                schema: "zpi",
                table: "Transactions");

            migrationBuilder.RenameTable(
                name: "Wallets",
                schema: "zpi",
                newName: "WalletEntity",
                newSchema: "zpi");

            migrationBuilder.RenameTable(
                name: "Transactions",
                schema: "zpi",
                newName: "TransactionEntity",
                newSchema: "zpi");

            migrationBuilder.RenameIndex(
                name: "IX_Transactions_AssetIdentifier",
                schema: "zpi",
                table: "TransactionEntity",
                newName: "IX_TransactionEntity_AssetIdentifier");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WalletEntity",
                schema: "zpi",
                table: "WalletEntity",
                column: "Identifier");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TransactionEntity",
                schema: "zpi",
                table: "TransactionEntity",
                column: "Identifier");

            migrationBuilder.AddForeignKey(
                name: "FK_TransactionEntity_Assets_AssetIdentifier",
                schema: "zpi",
                table: "TransactionEntity",
                column: "AssetIdentifier",
                principalSchema: "zpi",
                principalTable: "Assets",
                principalColumn: "Identifier",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
