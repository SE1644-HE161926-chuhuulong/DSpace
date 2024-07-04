using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangeEntityCollectionGroup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Action",
                table: "CollectionGroups");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "CollectionGroups",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "CollectionGroups");

            migrationBuilder.AddColumn<int>(
                name: "Action",
                table: "CollectionGroups",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
