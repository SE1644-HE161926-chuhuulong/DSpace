using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemoveUnusedTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_Languages_LanguageId",
                table: "Items");

            migrationBuilder.DropForeignKey(
                name: "FK_Items_Metadatas_MetadataId",
                table: "Items");

            migrationBuilder.DropTable(
                name: "ItemKeywords");

            migrationBuilder.DropTable(
                name: "Languages");

            migrationBuilder.DropTable(
                name: "Metadatas");

            migrationBuilder.DropTable(
                name: "VersionItems");

            migrationBuilder.DropTable(
                name: "Keywords");

            migrationBuilder.DropIndex(
                name: "IX_Items_LanguageId",
                table: "Items");

            migrationBuilder.DropIndex(
                name: "IX_Items_MetadataId",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "Action",
                table: "CommunitiesGroups");

            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "CommunitiesGroups");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "CommunitiesGroups");

            migrationBuilder.DropColumn(
                name: "PolicyType",
                table: "CommunitiesGroups");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "CommunitiesGroups");

            migrationBuilder.DropColumn(
                name: "isActive",
                table: "CommunitiesGroups");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "CollectionGroups");

            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "CollectionGroups");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "CollectionGroups");

            migrationBuilder.DropColumn(
                name: "PolicyType",
                table: "CollectionGroups");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "CollectionGroups");

            migrationBuilder.DropColumn(
                name: "isActive",
                table: "CollectionGroups");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Action",
                table: "CommunitiesGroups",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "CommunitiesGroups",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "CommunitiesGroups",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PolicyType",
                table: "CommunitiesGroups",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDate",
                table: "CommunitiesGroups",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "isActive",
                table: "CommunitiesGroups",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "CollectionGroups",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "CollectionGroups",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "CollectionGroups",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PolicyType",
                table: "CollectionGroups",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDate",
                table: "CollectionGroups",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "isActive",
                table: "CollectionGroups",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "Keywords",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KeywordName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Keywords", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Languages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LanguageCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LanguageName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Languages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Metadatas",
                columns: table => new
                {
                    MetadataId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Contributor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Coverage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Format = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Identifier = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Language = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Publisher = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Relation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Rights = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Source = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Subject = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Metadatas", x => x.MetadataId);
                });

            migrationBuilder.CreateTable(
                name: "VersionItems",
                columns: table => new
                {
                    VersionItemId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemId = table.Column<int>(type: "int", nullable: false),
                    PeopleId = table.Column<int>(type: "int", nullable: false),
                    VersionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    VersionNumber = table.Column<int>(type: "int", nullable: false),
                    VersionSummary = table.Column<string>(type: "nvarchar(max)", nullable: true)
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

            migrationBuilder.CreateTable(
                name: "ItemKeywords",
                columns: table => new
                {
                    ItemId = table.Column<int>(type: "int", nullable: false),
                    KeywordId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemKeywords", x => new { x.ItemId, x.KeywordId });
                    table.ForeignKey(
                        name: "FK_ItemKeywords_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "ItemId");
                    table.ForeignKey(
                        name: "FK_ItemKeywords_Keywords_KeywordId",
                        column: x => x.KeywordId,
                        principalTable: "Keywords",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Items_LanguageId",
                table: "Items",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_Items_MetadataId",
                table: "Items",
                column: "MetadataId",
                unique: true,
                filter: "[MetadataId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ItemKeywords_KeywordId",
                table: "ItemKeywords",
                column: "KeywordId");

            migrationBuilder.CreateIndex(
                name: "IX_VersionItems_ItemId",
                table: "VersionItems",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_VersionItems_PeopleId",
                table: "VersionItems",
                column: "PeopleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Languages_LanguageId",
                table: "Items",
                column: "LanguageId",
                principalTable: "Languages",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Metadatas_MetadataId",
                table: "Items",
                column: "MetadataId",
                principalTable: "Metadatas",
                principalColumn: "MetadataId");
        }
    }
}
