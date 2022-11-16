using Microsoft.EntityFrameworkCore.Migrations;
using NodaTime;

#nullable disable

namespace ZPI.Persistance.ZPIDb.Migrations
{
    public partial class ChangeWalletDateTimeToDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TimeStamp",
                schema: "zpi",
                table: "Wallets");

            migrationBuilder.AddColumn<LocalDate>(
                name: "DateStamp",
                schema: "zpi",
                table: "Wallets",
                type: "date",
                nullable: false,
                defaultValue: new NodaTime.LocalDate(1, 1, 1));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateStamp",
                schema: "zpi",
                table: "Wallets");

            migrationBuilder.AddColumn<OffsetDateTime>(
                name: "TimeStamp",
                schema: "zpi",
                table: "Wallets",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new NodaTime.OffsetDateTime(new NodaTime.LocalDateTime(1, 1, 1, 0, 0), NodaTime.Offset.FromHours(0)));
        }
    }
}
