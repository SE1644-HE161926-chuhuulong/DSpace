using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddNewEntityCollectionGroup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CollectionGroups",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    PolicyType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Action = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CollectionId = table.Column<int>(type: "int", nullable: true),
                    GroupId = table.Column<int>(type: "int", nullable: true),
                    isActive = table.Column<bool>(type: "bit", nullable: false),
                    canReview = table.Column<bool>(type: "bit", nullable: false),
                    canSubmit = table.Column<bool>(type: "bit", nullable: false),
                    canEdit = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CollectionGroups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CollectionGroups_Collections_CollectionId",
                        column: x => x.CollectionId,
                        principalTable: "Collections",
                        principalColumn: "CollectionId");
                    table.ForeignKey(
                        name: "FK_CollectionGroups_Groups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Groups",
                        principalColumn: "GroupId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_CollectionGroups_CollectionId",
                table: "CollectionGroups",
                column: "CollectionId");

            migrationBuilder.CreateIndex(
                name: "IX_CollectionGroups_GroupId",
                table: "CollectionGroups",
                column: "GroupId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CollectionGroups");
        }
    }
}
