using Microsoft.EntityFrameworkCore.Migrations;

namespace NewsAggregator.Data.Migrations
{
    public partial class testmigration2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "SiteNames",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "Abc");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "SiteNames",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: null);
        }
    }
}
