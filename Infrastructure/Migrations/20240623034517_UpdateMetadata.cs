using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateMetadata : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Abstract",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "Citation",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "OtherTitle",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "SeriesNo",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "Sponsors",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Items");

            migrationBuilder.RenameColumn(
                name: "isActive",
                table: "Items",
                newName: "Discoverable");

            migrationBuilder.RenameColumn(
                name: "DateOfIssue",
                table: "Items",
                newName: "LastModified");

            migrationBuilder.AddColumn<int>(
                name: "SubmitterId",
                table: "Items",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "MetadataFieldRegistries",
                columns: table => new
                {
                    MetadataFieldId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Element = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Qualifier = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ScopeNote = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MetadataFieldRegistries", x => x.MetadataFieldId);
                });

            migrationBuilder.CreateTable(
                name: "MetadataValues",
                columns: table => new
                {
                    MetadataValueId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TextValue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TextLang = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MetadataFieldId = table.Column<int>(type: "int", nullable: false),
                    ItemId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MetadataValues", x => x.MetadataValueId);
                    table.ForeignKey(
                        name: "FK_MetadataValues_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "ItemId");
                    table.ForeignKey(
                        name: "FK_MetadataValues_MetadataFieldRegistries_MetadataFieldId",
                        column: x => x.MetadataFieldId,
                        principalTable: "MetadataFieldRegistries",
                        principalColumn: "MetadataFieldId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Items_SubmitterId",
                table: "Items",
                column: "SubmitterId");

            migrationBuilder.CreateIndex(
                name: "IX_MetadataValues_ItemId",
                table: "MetadataValues",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_MetadataValues_MetadataFieldId",
                table: "MetadataValues",
                column: "MetadataFieldId");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Peoples_SubmitterId",
                table: "Items",
                column: "SubmitterId",
                principalTable: "Peoples",
                principalColumn: "PeopleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_Peoples_SubmitterId",
                table: "Items");

            migrationBuilder.DropTable(
                name: "MetadataValues");

            migrationBuilder.DropTable(
                name: "MetadataFieldRegistries");

            migrationBuilder.DropIndex(
                name: "IX_Items_SubmitterId",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "SubmitterId",
                table: "Items");

            migrationBuilder.RenameColumn(
                name: "LastModified",
                table: "Items",
                newName: "DateOfIssue");

            migrationBuilder.RenameColumn(
                name: "Discoverable",
                table: "Items",
                newName: "isActive");

            migrationBuilder.AddColumn<string>(
                name: "Abstract",
                table: "Items",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Citation",
                table: "Items",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OtherTitle",
                table: "Items",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SeriesNo",
                table: "Items",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Sponsors",
                table: "Items",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "Items",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
