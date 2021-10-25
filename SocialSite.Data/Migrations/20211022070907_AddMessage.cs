using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SocialSite.Data.Migrations
{
    public partial class AddMessage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Messages",
                columns: table => new
                {
                    MessageId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    OriginalId = table.Column<string>(nullable: true),
                    Text = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    ScreenName = table.Column<string>(nullable: true),
                    Utc = table.Column<DateTimeOffset>(nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(nullable: false),
                    Favorites = table.Column<int>(nullable: false),
                    Shares = table.Column<int>(nullable: false),
                    Language = table.Column<string>(nullable: true),
                    Client = table.Column<string>(nullable: true),
                    Type = table.Column<string>(nullable: true),
                    Urls = table.Column<string>(nullable: true),
                    Hashtags = table.Column<string>(nullable: true),
                    Mentions = table.Column<string>(nullable: true),
                    MediaType = table.Column<string>(nullable: true),
                    MediaUrls = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messages", x => x.MessageId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Messages");
        }
    }
}
