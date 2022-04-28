using Microsoft.EntityFrameworkCore.Migrations;
using System.IO;

namespace NewsAggregator.Data.Migrations
{
    public partial class ADDRSSFEEDSFROMSQLSCRIPT : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sqlScriptPath = @"../NewsAggregator.Data/SeedData/populate_rssfeeds.sql";
            migrationBuilder.Sql(File.ReadAllText(sqlScriptPath));
        }

        protected override void Down(MigrationBuilder migrationBuilder) { }
    }
}
