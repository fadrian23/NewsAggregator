using Microsoft.EntityFrameworkCore.Migrations;

namespace NewsAggregator.Data.Migrations
{
    public partial class correctmanytomanyrelationbetweenUserSettingsandRssFeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RssFeeds_ApplicationUserSettings_ApplicationUserSettingsId",
                table: "RssFeeds");

            migrationBuilder.DropIndex(
                name: "IX_RssFeeds_ApplicationUserSettingsId",
                table: "RssFeeds");

            migrationBuilder.DropColumn(
                name: "ApplicationUserSettingsId",
                table: "RssFeeds");

            migrationBuilder.CreateTable(
                name: "ApplicationUserSettingsRssFeed",
                columns: table => new
                {
                    RssFeedsRssFeedId = table.Column<int>(type: "int", nullable: false),
                    UserSettingsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUserSettingsRssFeed", x => new { x.RssFeedsRssFeedId, x.UserSettingsId });
                    table.ForeignKey(
                        name: "FK_ApplicationUserSettingsRssFeed_ApplicationUserSettings_UserSettingsId",
                        column: x => x.UserSettingsId,
                        principalTable: "ApplicationUserSettings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ApplicationUserSettingsRssFeed_RssFeeds_RssFeedsRssFeedId",
                        column: x => x.RssFeedsRssFeedId,
                        principalTable: "RssFeeds",
                        principalColumn: "RssFeedId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUserSettingsRssFeed_UserSettingsId",
                table: "ApplicationUserSettingsRssFeed",
                column: "UserSettingsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApplicationUserSettingsRssFeed");

            migrationBuilder.AddColumn<int>(
                name: "ApplicationUserSettingsId",
                table: "RssFeeds",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_RssFeeds_ApplicationUserSettingsId",
                table: "RssFeeds",
                column: "ApplicationUserSettingsId");

            migrationBuilder.AddForeignKey(
                name: "FK_RssFeeds_ApplicationUserSettings_ApplicationUserSettingsId",
                table: "RssFeeds",
                column: "ApplicationUserSettingsId",
                principalTable: "ApplicationUserSettings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
