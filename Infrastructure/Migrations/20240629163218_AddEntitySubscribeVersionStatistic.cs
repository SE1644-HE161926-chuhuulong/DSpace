using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddEntitySubscribeVersionStatistic : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DownloadCount",
                table: "FileUploads",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Statistics",
                columns: table => new
                {
                    StatisticId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Month = table.Column<int>(type: "int", nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false),
                    ViewCount = table.Column<int>(type: "int", nullable: false),
                    ViewOnDay = table.Column<int>(type: "int", nullable: false),
                    ItemId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Statistics", x => x.StatisticId);
                    table.ForeignKey(
                        name: "FK_Statistics_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "ItemId");
                });

            migrationBuilder.CreateTable(
                name: "Subscribes",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CollectionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subscribes", x => new { x.UserId, x.CollectionId });
                    table.ForeignKey(
                        name: "FK_Subscribes_Collections_CollectionId",
                        column: x => x.CollectionId,
                        principalTable: "Collections",
                        principalColumn: "CollectionId");
                    table.ForeignKey(
                        name: "FK_Subscribes_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "VersionItems",
                columns: table => new
                {
                    VersionItemId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VersionNumber = table.Column<int>(type: "int", nullable: false),
                    VersionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    VersionSummary = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PeopleId = table.Column<int>(type: "int", nullable: false),
                    ItemId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VersionItems", x => x.VersionItemId);
                    table.ForeignKey(
                        name: "FK_VersionItems_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "ItemId");
                    table.ForeignKey(
                        name: "FK_VersionItems_Peoples_PeopleId",
                        column: x => x.PeopleId,
                        principalTable: "Peoples",
                        principalColumn: "PeopleId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Statistics_ItemId",
                table: "Statistics",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_Subscribes_CollectionId",
                table: "Subscribes",
                column: "CollectionId");

            migrationBuilder.CreateIndex(
                name: "IX_VersionItems_ItemId",
                table: "VersionItems",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_VersionItems_PeopleId",
                table: "VersionItems",
                column: "PeopleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Statistics");

            migrationBuilder.DropTable(
                name: "Subscribes");

            migrationBuilder.DropTable(
                name: "VersionItems");

            migrationBuilder.DropColumn(
                name: "DownloadCount",
                table: "FileUploads");
        }
    }
}
