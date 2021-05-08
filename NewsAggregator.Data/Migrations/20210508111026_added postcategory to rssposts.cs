using Microsoft.EntityFrameworkCore.Migrations;

namespace NewsAggregator.Data.Migrations
{
    public partial class addedpostcategorytorssposts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Date",
                table: "InformationSitesPosts",
                newName: "DateTime");

            migrationBuilder.AddColumn<int>(
                name: "PostCategoryId",
                table: "InformationSitesPosts",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_InformationSitesPosts_PostCategoryId",
                table: "InformationSitesPosts",
                column: "PostCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_InformationSitesPosts_PostCategories_PostCategoryId",
                table: "InformationSitesPosts",
                column: "PostCategoryId",
                principalTable: "PostCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InformationSitesPosts_PostCategories_PostCategoryId",
                table: "InformationSitesPosts");

            migrationBuilder.DropIndex(
                name: "IX_InformationSitesPosts_PostCategoryId",
                table: "InformationSitesPosts");

            migrationBuilder.DropColumn(
                name: "PostCategoryId",
                table: "InformationSitesPosts");

            migrationBuilder.RenameColumn(
                name: "DateTime",
                table: "InformationSitesPosts",
                newName: "Date");
        }
    }
}
