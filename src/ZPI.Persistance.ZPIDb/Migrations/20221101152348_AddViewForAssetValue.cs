using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZPI.Persistance.ZPIDb.Migrations
{
    public partial class AddViewForAssetValue : Migration
    {
      protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                create or replace view zpi.""AssetValueAtm"" as
                select a1.""AssetIdentifier"", a1.""Value"", a1.""TimeStamp"" 
                from zpi.""AssetValues"" a1
                inner join 
                (
                SELECT ""AssetIdentifier"" , max(""TimeStamp"") as mts
                FROM zpi.""AssetValues"" a2
                GROUP BY ""AssetIdentifier""
                ) a2 on a2.""AssetIdentifier"" = a1.""AssetIdentifier"" and a1.""TimeStamp"" = a2.mts
            ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                drop view AssetValueAtm;
            ");
        }
    }
}
