using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FileUploadAndUpdateDatetime2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AuthorItem_Authors_AuthorId",
                table: "AuthorItem");

            migrationBuilder.DropForeignKey(
                name: "FK_AuthorItem_Items_ItemId",
                table: "AuthorItem");

            migrationBuilder.DropForeignKey(
                name: "FK_ItemKeyword_Items_ItemId",
                table: "ItemKeyword");

            migrationBuilder.DropForeignKey(
                name: "FK_ItemKeyword_Keyword_KeywordId",
                table: "ItemKeyword");

            migrationBuilder.DropForeignKey(
                name: "FK_Items_Language_LanguageId",
                table: "Items");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Language",
                table: "Language");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Keyword",
                table: "Keyword");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ItemKeyword",
                table: "ItemKeyword");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AuthorItem",
                table: "AuthorItem");

            migrationBuilder.RenameTable(
                name: "Language",
                newName: "Languages");

            migrationBuilder.RenameTable(
                name: "Keyword",
                newName: "Keywords");

            migrationBuilder.RenameTable(
                name: "ItemKeyword",
                newName: "ItemKeywords");

            migrationBuilder.RenameTable(
                name: "AuthorItem",
                newName: "AuthorItems");

            migrationBuilder.RenameColumn(
                name: "FileKey",
                table: "FileUploads",
                newName: "MimeType");

            migrationBuilder.RenameIndex(
                name: "IX_ItemKeyword_KeywordId",
                table: "ItemKeywords",
                newName: "IX_ItemKeywords_KeywordId");

            migrationBuilder.RenameIndex(
                name: "IX_AuthorItem_AuthorId",
                table: "AuthorItems",
                newName: "IX_AuthorItems_AuthorId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateOfIssue",
                table: "Items",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationTime",
                table: "FileUploads",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "FileKeyId",
                table: "FileUploads",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FileName",
                table: "FileUploads",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Kind",
                table: "FileUploads",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "BirthDate",
                table: "Authors",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Languages",
                table: "Languages",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Keywords",
                table: "Keywords",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ItemKeywords",
                table: "ItemKeywords",
                columns: new[] { "ItemId", "KeywordId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_AuthorItems",
                table: "AuthorItems",
                columns: new[] { "ItemId", "AuthorId" });

            migrationBuilder.AddForeignKey(
                name: "FK_AuthorItems_Authors_AuthorId",
                table: "AuthorItems",
                column: "AuthorId",
                principalTable: "Authors",
                principalColumn: "AuthorId");

            migrationBuilder.AddForeignKey(
                name: "FK_AuthorItems_Items_ItemId",
                table: "AuthorItems",
                column: "ItemId",
                principalTable: "Items",
                principalColumn: "ItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_ItemKeywords_Items_ItemId",
                table: "ItemKeywords",
                column: "ItemId",
                principalTable: "Items",
                principalColumn: "ItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_ItemKeywords_Keywords_KeywordId",
                table: "ItemKeywords",
                column: "KeywordId",
                principalTable: "Keywords",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Languages_LanguageId",
                table: "Items",
                column: "LanguageId",
                principalTable: "Languages",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AuthorItems_Authors_AuthorId",
                table: "AuthorItems");

            migrationBuilder.DropForeignKey(
                name: "FK_AuthorItems_Items_ItemId",
                table: "AuthorItems");

            migrationBuilder.DropForeignKey(
                name: "FK_ItemKeywords_Items_ItemId",
                table: "ItemKeywords");

            migrationBuilder.DropForeignKey(
                name: "FK_ItemKeywords_Keywords_KeywordId",
                table: "ItemKeywords");

            migrationBuilder.DropForeignKey(
                name: "FK_Items_Languages_LanguageId",
                table: "Items");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Languages",
                table: "Languages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Keywords",
                table: "Keywords");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ItemKeywords",
                table: "ItemKeywords");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AuthorItems",
                table: "AuthorItems");

            migrationBuilder.DropColumn(
                name: "CreationTime",
                table: "FileUploads");

            migrationBuilder.DropColumn(
                name: "FileKeyId",
                table: "FileUploads");

            migrationBuilder.DropColumn(
                name: "FileName",
                table: "FileUploads");

            migrationBuilder.DropColumn(
                name: "Kind",
                table: "FileUploads");

            migrationBuilder.RenameTable(
                name: "Languages",
                newName: "Language");

            migrationBuilder.RenameTable(
                name: "Keywords",
                newName: "Keyword");

            migrationBuilder.RenameTable(
                name: "ItemKeywords",
                newName: "ItemKeyword");

            migrationBuilder.RenameTable(
                name: "AuthorItems",
                newName: "AuthorItem");

            migrationBuilder.RenameColumn(
                name: "MimeType",
                table: "FileUploads",
                newName: "FileKey");

            migrationBuilder.RenameIndex(
                name: "IX_ItemKeywords_KeywordId",
                table: "ItemKeyword",
                newName: "IX_ItemKeyword_KeywordId");

            migrationBuilder.RenameIndex(
                name: "IX_AuthorItems_AuthorId",
                table: "AuthorItem",
                newName: "IX_AuthorItem_AuthorId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateOfIssue",
                table: "Items",
                type: "datetime",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "BirthDate",
                table: "Authors",
                type: "datetime",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Language",
                table: "Language",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Keyword",
                table: "Keyword",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ItemKeyword",
                table: "ItemKeyword",
                columns: new[] { "ItemId", "KeywordId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_AuthorItem",
                table: "AuthorItem",
                columns: new[] { "ItemId", "AuthorId" });

            migrationBuilder.AddForeignKey(
                name: "FK_AuthorItem_Authors_AuthorId",
                table: "AuthorItem",
                column: "AuthorId",
                principalTable: "Authors",
                principalColumn: "AuthorId");

            migrationBuilder.AddForeignKey(
                name: "FK_AuthorItem_Items_ItemId",
                table: "AuthorItem",
                column: "ItemId",
                principalTable: "Items",
                principalColumn: "ItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_ItemKeyword_Items_ItemId",
                table: "ItemKeyword",
                column: "ItemId",
                principalTable: "Items",
                principalColumn: "ItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_ItemKeyword_Keyword_KeywordId",
                table: "ItemKeyword",
                column: "KeywordId",
                principalTable: "Keyword",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Language_LanguageId",
                table: "Items",
                column: "LanguageId",
                principalTable: "Language",
                principalColumn: "Id");
        }
    }
}
