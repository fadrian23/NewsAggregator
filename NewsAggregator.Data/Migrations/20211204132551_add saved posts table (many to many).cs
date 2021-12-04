using Microsoft.EntityFrameworkCore.Migrations;

namespace NewsAggregator.Data.Migrations
{
    public partial class addsavedpoststablemanytomany : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ApplicationUserSettingsRssPost",
                columns: table => new
                {
                    ApplicationUserSettingsId = table.Column<int>(type: "int", nullable: false),
                    SavedPostsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUserSettingsRssPost", x => new { x.ApplicationUserSettingsId, x.SavedPostsId });
                    table.ForeignKey(
                        name: "FK_ApplicationUserSettingsRssPost_ApplicationUserSettings_ApplicationUserSettingsId",
                        column: x => x.ApplicationUserSettingsId,
                        principalTable: "ApplicationUserSettings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ApplicationUserSettingsRssPost_InformationSitesPosts_SavedPostsId",
                        column: x => x.SavedPostsId,
                        principalTable: "InformationSitesPosts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUserSettingsRssPost_SavedPostsId",
                table: "ApplicationUserSettingsRssPost",
                column: "SavedPostsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApplicationUserSettingsRssPost");
        }
    }
}
