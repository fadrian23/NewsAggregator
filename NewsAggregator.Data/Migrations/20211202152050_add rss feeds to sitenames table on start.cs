using Microsoft.EntityFrameworkCore.Migrations;

namespace NewsAggregator.Data.Migrations
{
    public partial class addrssfeedstositenamestableonstart : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "SiteNames",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "PolsatNews");

            migrationBuilder.InsertData(
                table: "SiteNames",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 29, "Interia_Gry" },
                    { 28, "Interia_Menway" },
                    { 27, "Interia_Kobieta" },
                    { 26, "Interia_Sport" },
                    { 25, "Interia_Opinie" },
                    { 24, "Interia_Autorzy" },
                    { 23, "Interia_Ciekawostki" },
                    { 22, "Interia_Religia" },
                    { 21, "Interia_Nauka" },
                    { 20, "Interia_Historia" },
                    { 19, "Interia_Kultura" },
                    { 18, "Interia_Zagranica" },
                    { 17, "Interia_Swiat" },
                    { 16, "Interia_Wywiady" },
                    { 15, "Interia_Polska" },
                    { 14, "Interia" },
                    { 13, "WP" },
                    { 12, "Onet" },
                    { 11, "Tvn24" },
                    { 10, "PolsatNews_CzystaPolska" },
                    { 9, "PolsatNews_Sport" },
                    { 8, "PolsatNews_Kultura" },
                    { 7, "PolsatNews_Moto" },
                    { 6, "PolsatNews_Technologie" },
                    { 5, "PolsatNews_Biznes" },
                    { 4, "PolsatNews_Wideo" },
                    { 3, "PolsatNews_Swiat" },
                    { 30, "Interia_NoweTechnologie" },
                    { 2, "PolsatNews_Polska" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "SiteNames",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "SiteNames",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "SiteNames",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "SiteNames",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "SiteNames",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "SiteNames",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "SiteNames",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "SiteNames",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "SiteNames",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "SiteNames",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "SiteNames",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "SiteNames",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "SiteNames",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "SiteNames",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "SiteNames",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "SiteNames",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "SiteNames",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "SiteNames",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "SiteNames",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "SiteNames",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "SiteNames",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "SiteNames",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "SiteNames",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "SiteNames",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "SiteNames",
                keyColumn: "Id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "SiteNames",
                keyColumn: "Id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "SiteNames",
                keyColumn: "Id",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "SiteNames",
                keyColumn: "Id",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "SiteNames",
                keyColumn: "Id",
                keyValue: 30);

            migrationBuilder.UpdateData(
                table: "SiteNames",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "Abc");
        }
    }
}
