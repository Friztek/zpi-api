using Microsoft.EntityFrameworkCore.Migrations;
using NodaTime;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ZPI.Persistance.ZPIDb.Migrations
{
    public partial class WalletEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Assets_AssetIdentifier",
                schema: "zpi",
                table: "Transactions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Transactions",
                schema: "zpi",
                table: "Transactions");

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
                name: "PK_TransactionEntity",
                schema: "zpi",
                table: "TransactionEntity",
                column: "Identifier");

            migrationBuilder.CreateTable(
                name: "WalletEntity",
                schema: "zpi",
                columns: table => new
                {
                    Identifier = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserIdentifier = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<double>(type: "double precision", nullable: false),
                    TimeStamp = table.Column<OffsetDateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WalletEntity", x => x.Identifier);
                });

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TransactionEntity_Assets_AssetIdentifier",
                schema: "zpi",
                table: "TransactionEntity");

            migrationBuilder.DropTable(
                name: "WalletEntity",
                schema: "zpi");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TransactionEntity",
                schema: "zpi",
                table: "TransactionEntity");

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
    }
}
