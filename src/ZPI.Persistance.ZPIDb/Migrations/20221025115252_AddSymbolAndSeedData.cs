using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZPI.Persistance.ZPIDb.Migrations
{
    public partial class AddSymbolAndSeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Symbol",
                schema: "zpi",
                table: "Assets",
                type: "text",
                nullable: true);

            migrationBuilder.InsertData(
                schema: "zpi",
                table: "Assets",
                columns: new[] { "Identifier", "Category", "FriendlyName", "Symbol" },
                values: new object[,]
                {
                    { "btc", "crypto", "Bitcoin", null },
                    { "cad", "currency", "Canadian Dollar", "$" },
                    { "chf", "currency", "Swiss Franc", "CHf" },
                    { "czk", "currency", "Czech Koruna", "Kč" },
                    { "eth", "crypto", "Ethereum", null },
                    { "eur", "currency", "Euro", "€" },
                    { "gbp", "currency", "Pound sterling", "£" },
                    { "gold", "metal", "Gold", null },
                    { "hrk", "currency", "Croatian Kuna", "kn" },
                    { "huf", "currency", "Hungarian Forint", "Ft" },
                    { "inr", "currency", "Indian Rupee", "₹" },
                    { "jpy", "currency", "Japanese Yen", "¥" },
                    { "ltc", "crypto", "Litecoin", null },
                    { "nok", "currency", "Norwegian Krone", "kr" },
                    { "platinum", "metal", "Platinum", null },
                    { "pln", "currency", "Polish złoty", "zł" },
                    { "rub", "currency", "Russian Ruble", "₽" },
                    { "sek", "currency", "Swedish Krona", "kr" },
                    { "silver", "metal", "Silver", null },
                    { "try", "currency", "Turkish lira", "₺" },
                    { "usd", "currency", "United States Dollar", "$" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "zpi",
                table: "Assets",
                keyColumn: "Identifier",
                keyValue: "btc");

            migrationBuilder.DeleteData(
                schema: "zpi",
                table: "Assets",
                keyColumn: "Identifier",
                keyValue: "cad");

            migrationBuilder.DeleteData(
                schema: "zpi",
                table: "Assets",
                keyColumn: "Identifier",
                keyValue: "chf");

            migrationBuilder.DeleteData(
                schema: "zpi",
                table: "Assets",
                keyColumn: "Identifier",
                keyValue: "czk");

            migrationBuilder.DeleteData(
                schema: "zpi",
                table: "Assets",
                keyColumn: "Identifier",
                keyValue: "eth");

            migrationBuilder.DeleteData(
                schema: "zpi",
                table: "Assets",
                keyColumn: "Identifier",
                keyValue: "eur");

            migrationBuilder.DeleteData(
                schema: "zpi",
                table: "Assets",
                keyColumn: "Identifier",
                keyValue: "gbp");

            migrationBuilder.DeleteData(
                schema: "zpi",
                table: "Assets",
                keyColumn: "Identifier",
                keyValue: "gold");

            migrationBuilder.DeleteData(
                schema: "zpi",
                table: "Assets",
                keyColumn: "Identifier",
                keyValue: "hrk");

            migrationBuilder.DeleteData(
                schema: "zpi",
                table: "Assets",
                keyColumn: "Identifier",
                keyValue: "huf");

            migrationBuilder.DeleteData(
                schema: "zpi",
                table: "Assets",
                keyColumn: "Identifier",
                keyValue: "inr");

            migrationBuilder.DeleteData(
                schema: "zpi",
                table: "Assets",
                keyColumn: "Identifier",
                keyValue: "jpy");

            migrationBuilder.DeleteData(
                schema: "zpi",
                table: "Assets",
                keyColumn: "Identifier",
                keyValue: "ltc");

            migrationBuilder.DeleteData(
                schema: "zpi",
                table: "Assets",
                keyColumn: "Identifier",
                keyValue: "nok");

            migrationBuilder.DeleteData(
                schema: "zpi",
                table: "Assets",
                keyColumn: "Identifier",
                keyValue: "platinum");

            migrationBuilder.DeleteData(
                schema: "zpi",
                table: "Assets",
                keyColumn: "Identifier",
                keyValue: "pln");

            migrationBuilder.DeleteData(
                schema: "zpi",
                table: "Assets",
                keyColumn: "Identifier",
                keyValue: "rub");

            migrationBuilder.DeleteData(
                schema: "zpi",
                table: "Assets",
                keyColumn: "Identifier",
                keyValue: "sek");

            migrationBuilder.DeleteData(
                schema: "zpi",
                table: "Assets",
                keyColumn: "Identifier",
                keyValue: "silver");

            migrationBuilder.DeleteData(
                schema: "zpi",
                table: "Assets",
                keyColumn: "Identifier",
                keyValue: "try");

            migrationBuilder.DeleteData(
                schema: "zpi",
                table: "Assets",
                keyColumn: "Identifier",
                keyValue: "usd");

            migrationBuilder.DropColumn(
                name: "Symbol",
                schema: "zpi",
                table: "Assets");
        }
    }
}
