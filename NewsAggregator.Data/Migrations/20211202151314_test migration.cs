using Microsoft.EntityFrameworkCore.Migrations;

namespace NewsAggregator.Data.Migrations
{
    public partial class testmigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "SiteNames",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.UpdateData(
                table: "SiteNames",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: null);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "SiteNames",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "Reddit");

            migrationBuilder.InsertData(
                table: "SiteNames",
                columns: new[] { "Id", "Name" },
                values: new object[] { 2, "HackerNews" });
        }
    }
}
