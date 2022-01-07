using Microsoft.EntityFrameworkCore.Migrations;

namespace NewsAggregator.Data.Migrations
{
    public partial class removeunusedparts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InformationSitesPosts_PostCategory_PostCategoryId",
                table: "InformationSitesPosts");

            migrationBuilder.DropTable(
                name: "Keyword");

            migrationBuilder.DropTable(
                name: "PostCategory");

            migrationBuilder.DropIndex(
                name: "IX_InformationSitesPosts_PostCategoryId",
                table: "InformationSitesPosts");

            migrationBuilder.DropColumn(
                name: "PostCategoryId",
                table: "InformationSitesPosts");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PostCategoryId",
                table: "InformationSitesPosts",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "PostCategory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostCategory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Keyword",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PostCategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Keyword", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Keyword_PostCategory_PostCategoryId",
                        column: x => x.PostCategoryId,
                        principalTable: "PostCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InformationSitesPosts_PostCategoryId",
                table: "InformationSitesPosts",
                column: "PostCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Keyword_PostCategoryId",
                table: "Keyword",
                column: "PostCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_InformationSitesPosts_PostCategory_PostCategoryId",
                table: "InformationSitesPosts",
                column: "PostCategoryId",
                principalTable: "PostCategory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
